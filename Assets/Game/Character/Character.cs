using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : Object {
	public enum DirectionType {
		East, West, South, North, Max
	}

	public enum EquipPart
	{
		Weapon,
		LeftRing,
		RightRing,
		Neck,
		Head,
		Hand,
		Feet,
		Legs,
		Body,
		Max
	}

	public class StateData {
		public int health;
		public int stamina;
		public int attack;
		public int speed;
		public int defense;
		static public StateData operator + (StateData rhs, StateData lhs)
		{
			rhs.health += lhs.health;
			rhs.stamina += lhs.stamina;
			rhs.attack += lhs.attack;
			rhs.speed += lhs.speed;
			rhs.defense += lhs.defense;
			return rhs;
		}
	}

	public string name;
	public StatusBarInt health = new StatusBarInt();
	public StatusBarInt stamina = new StatusBarInt();
	public int sight = 4;
	public int attack;
	public int speed;
	public int defense;

	public List<BuffData> buffs = new List<BuffData> ();

	public EquipmentItemData[] items = new EquipmentItemData[(int)EquipPart.Max];
	public int updateTime = 0;
	public StateData state = null;
	public DirectionType direction;

	public int GetAttack() {
		int attack = this.attack;
		WeaponItemData weapon = (WeaponItemData)items [(int)EquipPart.Weapon];
		if (null != weapon) {
			WeaponItemInfo info = (WeaponItemInfo)(weapon.info);
			attack += info.attack;
		} 
		for (int i=0; i<buffs.Count; i++) {
			if (true == buffs[i].IsValid ()) {
				attack += buffs[i].ApplyBuff (this).attack;
			}
		}
		return attack;
	}
	public int GetAttackRange() {
		return 1;
	}
	public int GetDefense() {
		int defense = this.defense;
		for (int i=0; i<(int)items.Length; i++) {
			EquipmentItemData item = items[i];
			if(null == item || ItemInfo.Category.Armor != item.info.category)
			{
				continue;
			}
			ArmorItemData armor = (ArmorItemData)item;
			if(null != armor) {
				ArmorItemInfo info = (ArmorItemInfo)armor.info;
				defense += info.defense;
			}
		}
		for (int i=0; i<buffs.Count; i++) {
			if (true == buffs[i].IsValid ()) {
				defense += buffs[i].ApplyBuff (this).defense;
			}
		}
		return defense;
	}
	public StateData GetState()
	{
		if (Game.Instance.currentTurn <= updateTime) {
			return state;
		}
		updateTime = Game.Instance.currentTurn;

		state = new StateData();
		state.health = health;
		state.stamina = stamina;
		state.attack = attack;
		state.defense = defense;
		state.speed = speed;

		for (int i=0; i<(int)items.Length; i++) {
			EquipmentItemData item = items[i];
			if(null == item) {
				continue;
			}

			switch(item.info.category) {
			case ItemInfo.Category.Weapon :
			{
				WeaponItemData data = (WeaponItemData)item;
				WeaponItemInfo info = (WeaponItemInfo)data.info;
				state.attack += info.attack;
				state.speed += info.speed;

			}
			break;
			case ItemInfo.Category.Armor :
			{
				ArmorItemData data = (ArmorItemData)item;
				ArmorItemInfo info = (ArmorItemInfo)data.info;
				state.defense += info.defense;
				state.speed += info.speed;
			}
				break;
			}
		}

		for (int i=0; i<buffs.Count;) {
			if(false == buffs[i].IsValid())
			{
				((CharacterView)view).OnDetachBuff(buffs[i]);
				buffs.RemoveAt(i);
			}
			else {
				state += buffs[i].ApplyBuff(this);
				i++;
			}
		}

		return state;
	}

	public void EquipItem(EquipmentItemData item, EquipPart part) {
		switch (item.info.category) {
		case ItemInfo.Category.Weapon :
			items[(int)EquipPart.Weapon] = item;
			break;
		case ItemInfo.Category.Armor : 
			EquipArmorItem((ArmorItemData)item, part);
			break;
		}
	}

	private void EquipArmorItem(ArmorItemData item, EquipPart part)	{
		ArmorItemInfo info = item.info as ArmorItemInfo;
		switch (info.type) {
		case ArmorItemInfo.ItemType.Body :
			if(part != EquipPart.Body)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Feet :
			if(part != EquipPart.Feet)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Hand :
			if(part != EquipPart.Hand)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Head :
			if(part != EquipPart.Head)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Legs :
			if(part != EquipPart.Legs)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Neck :
			if(part != EquipPart.Neck)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		case ArmorItemInfo.ItemType.Ring :
			if(part != EquipPart.LeftRing && part != EquipPart.RightRing)
			{
				throw new System.Exception("not proper item part(" +  info.name + ")");
			}
			break;
		default :
			throw new System.Exception("not proper item part(" +  info.name + ")");
		}

		items [(int)part] = item;
	}

	public void SetDamage(int damage) {
		//Buff.detach( this, Frost.class );
		
		//Class<?> srcClass = src.getClass();
		//if (immunities().contains( srcClass )) {
		//	dmg = 0;
		//} else if (resistances().contains( srcClass )) {
		//	dmg = Random.IntRange( 0, dmg );
		//}
		
//		if (buff( Paralysis.class ) != null) {
//			if (Random.Int( dmg ) >= Random.Int( HP )) {
//				Buff.detach( this, Paralysis.class );
//				if (Dungeon.visible[pos]) {
//					GLog.i( TXT_OUT_OF_PARALYSIS, name );
//				}
//			}
//		}
		
		health += (damage*-1);
//		if (dmg > 0 || src instanceof Char) {
//			sprite.showStatus( HP > HT / 2 ? 
//			                  CharSprite.WARNING : 
//			                  CharSprite.NEGATIVE,
//			                  Integer.toString( dmg ) );
//		}
		if (health <= 0) {
			Destroy();
		}
	}
	public virtual void Action() {
		GetState ();
	}
	public virtual void Destroy() {}

	public static StateData operator + (Character rhs, StateData lhs)
	{
		rhs.health += lhs.health;
		rhs.stamina += lhs.stamina;
		rhs.attack += lhs.attack;
		rhs.speed += lhs.speed;
		rhs.defense += lhs.defense;
		return lhs;
	}

	public static bool Hit(Character attacker, Character defender)
	{
		int attackerRoll = UnityEngine.Random.Range (0, attacker.GetAttack ());
		int defenderRoll = UnityEngine.Random.Range (0, defender.GetDefense ());
		return attackerRoll > defenderRoll;
	}

	public void Move(DirectionType direction) {
		Object.Position dest = new Object.Position (position.x, position.y);
		switch(direction) {
		case DirectionType.East : dest.x += 1; break;
		case DirectionType.West : dest.x -= 1; break;
		case DirectionType.North : dest.y -= 1; break;
		case DirectionType.South : dest.y += 1; break;
		}
		
		if (Game.Instance.map.width <= dest.x || 0 > dest.x || Game.Instance.map.height <= dest.y || 0 > dest.y) {
			return;
		}
		
		Tile tile = Game.Instance.map.GetTile (dest.x, dest.y);
		if (Tile.Type.Floor != tile.type) {
			return;
		}
		position = dest;
	}
}
