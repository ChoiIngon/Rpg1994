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
		if(null != tile)
		{
			List<KeyValuePair<Object, Object>> list = tile.objects.ToList ();
			// Loop over list.
			foreach (KeyValuePair<Object, Object> pair in list)
			{
				pair.Value.OnTrigger(this);
			}
		} 

		Util.Timer<Util.TurnCounter>.Instance.NextTime ();
		stamina -= 1;
	}

	public override void OnMove(Character.DirectionType direction)
	{
		view.SetPosition(position);
		
		DropItemView.Instance.gameObject.SetActive(false);
		Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
		
		List<ItemStack> stacks = new List<ItemStack> ();
		foreach (var v in tile.objects) {
			if(Object.Category.Item == v.Value.category)
			{
				stacks.Add ((ItemStack)v.Value);
			}
			if(5 <= stacks.Count())
			{
				break;
			}
		}
		if (0 < stacks.Count) {
			DropItemView.Instance.Init (stacks);
			DropItemView.Instance.gameObject.SetActive(true);
		}
	}
	public override void Update() {
		base.Update ();
		// Call ToList.
		{
			QuestData quest = QuestManager.Instance.GetAvailableQuest ();
			if (null != quest) {
				string script = "";
				foreach (QuestData.Dialouge dialouge in quest.startDialouges) {
					script += dialouge.speacker + ":\n\n";
					script += dialouge.script;
				}
				
				PopupMessageView.Instance.AddSubmitListener (() => {
					quest.Start ();
				});
				PopupMessageView.Instance.SetText (script);
				PopupMessageView.Instance.SetWidth (500);
				PopupMessageView.Instance.gameObject.SetActive (true);
			}
		}
		{
			QuestData quest = QuestManager.Instance.GetCompleteQuest ();
			if (null != quest) {
				string script = "";
				foreach (QuestData.Dialouge dialouge in quest.completeDialouges) {
					script += dialouge.speacker + ":\n\n";
					script += dialouge.script;
				}
				
				PopupMessageView.Instance.SetText (script);
				PopupMessageView.Instance.SetWidth (500);
				PopupMessageView.Instance.gameObject.SetActive (true);
			}
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
	}

	public override void OnDestroy()
	{
		//view.OnDestroy ();
	}

	public override void SetPosition(Object.Position position) {
		base.SetPosition (position);
		view.SetPosition (position);
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
		Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
		tile.view.CreateFloatingMessage ("-" + damage.ToString(), Color.red);
		LogView.Instance.Write ("당신은 " + damage + "의 피해를 입었습니다.");
	}
}
