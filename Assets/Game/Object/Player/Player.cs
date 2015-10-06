using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Character
{
	public MonsterData target = null;
	public Inventory inventory = null;
	public Player() {
		target = null;
		category = Object.Category.Player;
		inventory = new Inventory ();
	}
	public void EquipItem(int index, Character.EquipPart part) {
		ItemData item = inventory.Pull (index);
		EquipmentItemData equipedItem = items [(int)part];
		if (null != equipedItem) {
			inventory.Put (equipedItem);
		}
		base.EquipItem ((EquipmentItemData)item, part);
	}

	public void UnequipItem(Character.EquipPart part) {
		EquipmentItemData equipedItem = items [(int)part];
		if (null != equipedItem) {
			inventory.Put (equipedItem);
		}
		items [(int)part] = null;
	}

	public ItemStack DropItem(Character.EquipPart part) {
		EquipmentItemData item = items [(int)part];
		items [(int)part] = null;

		ItemStack itemStack = CreateItemStack (item, new Object.Position(position.x, position.y));
		return itemStack;
	}

	public Character.StateData UseItem(int index) {
		ItemData data = inventory.Pull(index);
		switch (data.info.category) {
		case ItemInfo.Category.Potion :
			PotionItemInfo info = (PotionItemInfo)data.info;
			Character.StateData state = new Character.StateData();
			foreach(BuffInfo buffInfo in info.buff)
			{
				BuffData buffData = buffInfo.CreateInstance();
				state += buffData.ApplyBuff(this);
				if(true == buffData.IsValid()) {
					buffs.Add (buffData);
				}
			}
			return state;
		default :
			throw new System.Exception("the item can not be used");
		}
	}
	private void CheckVisible(Object.Position dest)
	{
		List<Object.Position> positions = base.Raycast(dest);
		foreach(Object.Position position in positions) {
			if(sight < Vector2.Distance(this.position, position)) {
				return;
			}
			Tile tile = Game.Instance.map.GetTile (position.x, position.y);
			tile.visible = true;
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

		foreach(Tile tile in Game.Instance.map.tiles) {
			tile.visible = false;
			foreach(var v in tile.objects) {
				v.Value.visible =  false;
			}
		}

		{
			int y = Math.Max(0, src.y - sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Game.Instance.map.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int y = Math.Min(Game.Instance.map.height-1, src.y + sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Game.Instance.map.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Max(0, src.x - sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Game.Instance.map.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Min(Game.Instance.map.width-1, src.x + sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Game.Instance.map.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
	}
	public void MoveTo(Character.DirectionType direction) {
		this.direction = direction;
		this.target = null;
		base.Move (direction);		
		Game.Instance.gameTurn.NextTurn ();
	}
	public void Attack() {
		if (null == target) {
			for(int y=position.y - range; y<=position.y + range; y++) {
				for(int x=position.x - range; x<=position.x + range; x++) {
					if (Game.Instance.map.width <= x || 0 > x || Game.Instance.map.height <= y || 0 > y) {
						continue;
					}
					if(range < Vector2.Distance(position, new Object.Position(x, y))) {
						continue;
					}

					Tile tile = Game.Instance.map.GetTile (x, y);
					foreach(var v in tile.objects) {
						if(false == v.Value.visible) {
							continue;
						}
						if(Object.Category.Item == v.Value.category) {
							ItemStack itemStack = (ItemStack)v.Value;
							Game.Instance.player.inventory.Put (itemStack.item);
							//((PlayerView)view).OnPickupItem(itemStack.item);
							itemStack.Destroy();
							return;
						}
					}
				}
			}

			foreach (var v in MonsterManager.Instance.monsters) {
				MonsterData monster = v.Value;
				if(range >= Vector2.Distance(position, monster.position))
				{
					target = monster;
					break;
				}
			}
			if(null == target) {
				throw new System.Exception("no target selected");
			}
		}
		OnAttack(target, 0);
		if (true == Character.Hit (this, target)) {
			int damage = GetAttack ();
			int effectiveDamage = Math.Max (damage - target.GetDefense (), 0);
			target.SetDamage (this, effectiveDamage);
		} 
		if (0 >= target.health) {
			target = null;
		}
		Game.Instance.gameTurn.NextTurn ();
	}
	public override void Update() {
		base.Update ();
		FieldOfView ();
	}

	public override void OnAttack(Character defender, int damage) {
		LogView.Text ("당신은 ");
		MonsterData monster = (MonsterData)defender;
		LogView.Button ("<color=red>" + monster.name + "[" + monster.position.x + "," + monster.position.y + "]</color>", () => {
			InfoView.MonsterInfo(monster);
		});
		LogView.Text ("을(를) 공격합니다.\n");
	}
	public override void OnDamage(Character attacker, int damage) {
		LogView.Text ("당신은 " + damage + "의 피해를 입었습니다.\n");
	}
}
