using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : Character
{
	public MonsterData target = null;
	public Inventory inventory = new Inventory();

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
	public void DropItem(Character.EquipPart part) {
		items [(int)part] = null;
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
	private void Raycast(Object.Position dest)
	{
		List<Object.Position> positions = base.Raycast(dest);
		foreach(Object.Position position in positions) {
			Tile tile = Game.Instance.map.tiles[position.x + position.y * Game.Instance.map.width];
			tile.visible = true;
			foreach(var v in tile.dictObjects) {
				v.Value.visible = true;
			}
			if(Tile.Type.Floor != tile.type)
			{
				break;
			}
		}
	}
	public void FieldOfView() {
		Object.Position src = position;
		int sight = this.sight;
		foreach(Tile tile in Game.Instance.map.tiles) {
			tile.visible = false;
			foreach(var v in tile.dictObjects) {
				v.Value.visible = false;
			}
		}

		{
			int y = Math.Max(0, src.y - sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Game.Instance.map.width); x++)
			{
				Raycast(new Object.Position(x, y));
			}
		}
		{
			int y = Math.Min(Game.Instance.map.height-1, src.y + sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Game.Instance.map.width); x++)
			{
				Raycast(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Max(0, src.x - sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Game.Instance.map.height); y++)
			{
				Raycast(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Min(Game.Instance.map.width-1, src.x + sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Game.Instance.map.height); y++)
			{
				Raycast(new Object.Position(x, y));
			}
		}
		foreach(Tile tile in Game.Instance.map.tiles) {
			if(sight < Vector2.Distance(src, tile.position)) {
				tile.visible = false;
				foreach(var v in tile.dictObjects) {
					v.Value.visible = false;
				}
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
			foreach (var v in MonsterManager.Instance.monsters) {
				MonsterData monster = v.Value;
				if(GetAttackRange() >= Vector2.Distance(position, monster.position))
				{
					target = monster;
					break;
				}
			}
			if(null == target) {
				throw new System.Exception("no target selected");
			}
		}
		PlayerView view = (PlayerView)this.view;
		if (true == Character.Hit (this, target)) {
			int damage = GetAttack ();
			int effectiveDamage = Math.Max (damage - target.GetDefense (), 0);
			view.OnAttack(target, effectiveDamage);
			target.SetDamage (effectiveDamage);
		} else {
			view.OnAttack(target, 0);
		}
		if (0 >= target.health) {
			target = null;
		}
		Game.Instance.gameTurn.NextTurn ();
	}
	public override void Action() {
		base.Action ();
		FieldOfView ();
	}
}
