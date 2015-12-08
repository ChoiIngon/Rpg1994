using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : Object {
	private static Dictionary<EquipPart, ItemInfo.Category> ItemPartMap = new Dictionary<EquipPart, ItemInfo.Category>();
	static Character() {
		ItemPartMap.Add(EquipPart.Weapon, ItemInfo.Category.Weapon);
		ItemPartMap.Add(EquipPart.Shield, ItemInfo.Category.Shield);
		ItemPartMap.Add(EquipPart.LeftRing, ItemInfo.Category.Ring);
		ItemPartMap.Add(EquipPart.RightRing, ItemInfo.Category.Ring);
		ItemPartMap.Add(EquipPart.Shirt, ItemInfo.Category.Shirt);
	}
	public enum DirectionType {
		East, West, South, North, Max
	}
	public enum EquipPart
	{
		Weapon,
		Shield,
		LeftRing,
		RightRing,
		//Necklace,
		//Helmet,
		//Glove,
		//Shoes,
		//Pants,
		Shirt,
		Max
	}

	public class Status {
		public int health;
		public int stamina;
		public int attack;
		public int speed;
		public int defense;
		static public Status operator + (Status rhs, Status lhs)
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
	public Util.AutoRecoveryInt<Util.TurnCounter> health = new Util.AutoRecoveryInt<Util.TurnCounter>();
	public Util.AutoRecoveryInt<Util.TurnCounter> stamina = new Util.AutoRecoveryInt<Util.TurnCounter>();
	public int sight = 4;
	public int attack;
	public int speed;
	public int defense;
	public int range = 1;

	public List<BuffData> buffs = new List<BuffData> ();

	public EquipmentItemData[] items = new EquipmentItemData[(int)EquipPart.Max];
	public int updateTime = 0;
	public Status status = null;
	public DirectionType direction;
	
	public Status GetStatus()
	{
		if (GameManager.Instance.currentTurn <= updateTime) {
			return status;
		}
		updateTime = GameManager.Instance.currentTurn;

		status = new Status();
		status.health = health;
		status.stamina = stamina;
		status.attack = attack;
		status.defense = defense;
		status.speed = speed;

		for (int i=0; i<(int)items.Length; i++) {
			EquipmentItemData item = items[i];
			if(null == item) {
				continue;
			}
			status += item.GetStatus();
		}

		for (int i=0; i<buffs.Count;) {
			if(false == buffs[i].IsValid())
			{
				buffs.RemoveAt(i);
			}
			else {
				status += buffs[i].ApplyBuff(this);
				i++;
			}
		}

		return status;
	}

	public void EquipItem(EquipmentItemData item, EquipPart part) {
		if (item.info.category != Character.ItemPartMap [part]) {
			throw new System.Exception("can not equip " + item.info.name + " on " + part.ToString() + ", not proper item part");
		}
		items [(int)part] = item;
	}


	public void SetDamage(Character attacker, int damage) {
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
		OnDamage (attacker, damage);
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
	public virtual void Update() {
		GetStatus();
	}
	public override void Destroy() {
		base.Destroy ();
		OnDestroy ();
	}

	public static Status operator + (Character rhs, Status lhs)
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
		int attackerRoll = UnityEngine.Random.Range (0, attacker.GetStatus ().attack);
		int defenderRoll = UnityEngine.Random.Range (0, defender.GetStatus ().defense);
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
		
		if (GameManager.Instance.map.width <= dest.x || 0 > dest.x || GameManager.Instance.map.height <= dest.y || 0 > dest.y) {
			return;
		}

		{
			Tile tile = GameManager.Instance.map.GetTile (dest.x, dest.y);
			if (Tile.Type.Floor != tile.type) {
				return;
			}
			if(0 < tile.objects.Count) {
				return;
			}
			tile.AddObject(this);
		}
		{
			Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
			tile.RemoveObject(this);
		}
		position = dest;
		OnMove (direction);
	}

	public ItemStack CreateItemStack(ItemData item, Object.Position at) {
		ItemStack itemStack = new ItemStack ();
		itemStack.item = item;
		itemStack.SetPosition (at);
		return itemStack;
	}

	public virtual void OnCreate () {}
	public virtual void OnAttack(Character target, int damage) {}
	public virtual void OnDamage(Character attacker, int damage) {}
	public virtual void OnMove(Character.DirectionType direction) {}
	public virtual void OnEquipItem (ItemData item) {}
	public virtual void OnUnequipItem(ItemData item) {}
	public virtual void OnDestroy() {}
}
