using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : Character
{
	public Inventory inventory = null;
	public ObjectView view = null;
	public int level = 1;
	public int exp = 0;
	public Dictionary<string, QuestData> quests = null;

	public Player() {
		category = Object.Category.Player;
		inventory = new Inventory ();
		quests = new Dictionary<string, QuestData>();
	}
	public void EquipItem(int index, Character.EquipPart part) {
		ItemData item = inventory.Pull (index);
		EquipmentItemData equipedItem = equipments [(int)part];
		if (null != equipedItem) {
			inventory.Put (equipedItem);
		}
		base.EquipItem ((EquipmentItemData)item, part);
	}

	public void UnequipItem(Character.EquipPart part) {
		EquipmentItemData item = equipments [(int)part];
		if (null == item) {
			return;
		}
		inventory.Put(item);
		base.UnequipItem (part);
	}

	public ItemStack DropItem(Character.EquipPart part) {
		EquipmentItemData item = equipments [(int)part];
		if (null == item) {
			return null;
		}
		item.part = Character.EquipPart.Max;
		equipments [(int)part] = null;
		ItemStack stack = CreateItemStack (item, new Object.Position(position.x, position.y));
		OnDropItem (item);
		return stack;
	}

	public ItemStack DropItem(int index)
	{
		ItemData item = inventory.Pull (index);
		if (null == item) {
			return null;
		}
		ItemStack stack = CreateItemStack (item, new Object.Position (position.x, position.y));
		OnDropItem (item);
		return stack;
	}

	public void PickupItem(ItemStack stack)
	{
		GameManager.Instance.player.inventory.Put (stack.item);
		OnPickupItem (stack.item);
		stack.Destroy ();
	}

	public Character.Status UseItem(int index) {
		ItemData data = inventory.Pull(index);
		if (null == data) {
			throw new System.Exception("no item in slot");
		}
		return data.Use (this);
	}
	private void CheckVisible(Object.Position dest)
	{
		List<Object.Position> positions = base.Raycast(dest);
		foreach(Object.Position position in positions) {
			if(sight < Vector2.Distance(this.position, position)) {
				return;
			}
			Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
			tile.visible = true;
			tile.visit = true;
			foreach(var v in tile.objects) {
				v.Value.visible = true;
			}
			if(Tile.Type.Floor != tile.type)
			{
				return;
			}
		}
	}
	public void FieldOfView() {
		Object.Position src = position;
		int sight = this.sight;

		foreach(Tile tile in GameManager.Instance.map.tiles) {
			tile.visible = false;
			foreach(var v in tile.objects) {
				v.Value.visible =  false;
			}
		}

		{
			int y = Math.Max(0, src.y - sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, GameManager.Instance.map.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int y = Math.Min(GameManager.Instance.map.height-1, src.y + sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, GameManager.Instance.map.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Max(0, src.x - sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, GameManager.Instance.map.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Min(GameManager.Instance.map.width-1, src.x + sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, GameManager.Instance.map.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
	}
	public void MoveTo(Character.DirectionType direction) {
		this.direction = direction;

		Object.Position dest = new Object.Position (position.x, position.y);
		switch(direction) {
		case DirectionType.East : dest.x += 1; break;
		case DirectionType.West : dest.x -= 1; break;
		case DirectionType.North : dest.y -= 1; break;
		case DirectionType.South : dest.y += 1; break;
		}

		base.Move (dest);

		Tile tile = GameManager.Instance.map.GetTile (dest.x, dest.y);
		Object obj = this.target;
		if (null != obj && Object.Category.Monster == obj.category) {
			MonsterData monster = (MonsterData)obj;
			Attack (monster);
			stamina -= 1;
		} 

		FieldOfView ();
		Util.Timer<Util.TurnCounter>.Instance.NextTime ();
		stamina -= 1;
	}

	public override void Update() {
		base.Update ();
		List<string> completeQuestIDs = new List<string> ();
		foreach (var v in quests) {
			if(true == v.Value.IsComplete())
			{
				completeQuestIDs.Add (v.Value.id);
			}
		}
		foreach (string questID in completeQuestIDs) {
			quests.Remove(questID);
		}

		if (0 == stamina) {
			int damage = health.max/20;
			health -= damage;
			OnDamage(this, damage);
		}
	}
	
	public override void OnCreate()
	{
		view = ObjectView.Create<ObjectView> (this, "@", Color.green);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnMove(Character.DirectionType direction)
	{
		view.position = position;
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);

		DropItemView.Instance.gameObject.SetActive(false);
		Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);

		List<ItemStack> stacks = new List<ItemStack> ();
		foreach (var v in tile.objects) {
			if(Object.Category.Item == v.Value.category)
			{
				stacks.Add ((ItemStack)v.Value);
			}
		}
		if (0 < stacks.Count) {
			DropItemView.Instance.Init (stacks);
			DropItemView.Instance.gameObject.SetActive(true);
		}
	}

	public override void OnDropItem(ItemData item)
	{
		LogView.Instance.Write ("You dropped " + item.info.name);
	}

	public override void OnPickupItem(ItemData item)
	{
		LogView.Instance.Write ("Now you have <b>'" + item.info.name + "'</b>");
	}
	public override void OnAttack(Character defender) {
		MonsterData monster = (MonsterData)defender;
		LogView.Instance.Write ("당신은 <color=red>" + monster.name + "[" + monster.position.x + "," + monster.position.y + "]</color>을(를) 공격합니다.");
	}

	public override void OnDodge(Character attacker) {
		LogView.Instance.Write ("You dodge enemy's attack");
	}
	public override void OnDamage(Character attacker, int damage) {
		view.CreateFloatingMessage ("-" + damage.ToString(), Color.red);
		LogView.Instance.Write ("당신은 " + damage + "의 피해를 입었습니다.");
	}
}
