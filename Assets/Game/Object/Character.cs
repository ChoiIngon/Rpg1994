﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : Object {
	/*
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
	public int attack;
	public int speed;
	public int defense;
	public int range = 1;
	public List<BuffData> buffs = new List<BuffData> ();
	public EquipmentItemData[] equipments = new EquipmentItemData[(int)EquipPart.Max];
	public DirectionType direction;
	public Object target {
		get {
			Position dest = new Position (position.x, position.y);
			switch(direction) {
			case DirectionType.East : dest.x += 1; break;
			case DirectionType.West : dest.x -= 1; break;
			case DirectionType.North : dest.y -= 1; break;
			case DirectionType.South : dest.y += 1; break;
			}
			Tile tile = Map.Instance.GetTile(dest.x, dest.y);
			if(null == tile) {
				return null;
			}
			foreach(var v in tile.objects)
			{
				return v.Value;
			}
			return null;
		}
	}
	public Character() {
		size = 1.0f;
	}

	public Status GetStatus()
	{
		Status status = new Status();
		status.health = health;
		status.stamina = stamina;
		status.attack = attack;
		status.defense = defense;
		status.speed = speed;

		for (int i=0; i<(int)equipments.Length; i++) {
			EquipmentItemData item = equipments[i];
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
		item.part = part;
		equipments [(int)part] = item;
	}

	public EquipmentItemData UnequipItem(Character.EquipPart part) {
		EquipmentItemData equipedItem = equipments [(int)part];
		if (null == equipedItem) {
			return null;
		}
		equipedItem.part = Character.EquipPart.Max;
		equipments [(int)part] = null;
		return equipedItem;
	}

	public override void Update() {
		GetStatus ();
	}

	public bool IsVisible(Object dest) {
		if(sight < Vector2.Distance (new Vector2 (this.position.x, this.position.y), new Vector2 (dest.position.x, dest.position.y))) {
			return false;
		}

		List<Position> positions = Position.Raycast(this.position, dest.position);
		foreach(Position position in positions) {
			Tile tile = Map.Instance.GetTile (position.x, position.y);
			foreach(var v in tile.objects) {
				if(1.0f < v.Value.size)
				{
					return false;
				}
			}
		}
		return true;
	}

	public void Move(DirectionType direction) {
		Position dest = new Position (position.x, position.y);
		switch(direction) {
		case DirectionType.East : dest.x += 1; break;
		case DirectionType.West : dest.x -= 1; break;
		case DirectionType.North : dest.y -= 1; break;
		case DirectionType.South : dest.y += 1; break;
		}

		Move (dest);
	}

	public void Move(Position dest)
	{
		if (Map.Instance.width <= dest.x || 0 > dest.x || Map.Instance.height <= dest.y || 0 > dest.y) {
			return;
		}
		
		{
			Tile tile = Map.Instance.GetTile (dest.x, dest.y);
			float size = 0.0f;
			foreach(var v in tile.objects)
			{
				size += v.Value.size;
				if(1.0f <= size)
				{
					return;
				}
			}
			tile.AddObject(this);
		}
		{
			Tile tile = Map.Instance.GetTile (position.x, position.y);
			tile.RemoveObject(this);
		}
		position = dest;
		OnMove (direction);
	}

	private bool Hit(Character attacker, Character defender)
	{
		int attackerRoll = UnityEngine.Random.Range (0, attacker.GetStatus ().speed);
		int defenderRoll = UnityEngine.Random.Range (0, defender.GetStatus ().speed);
		return attackerRoll > defenderRoll;
	}

	public void Attack(Character target)
	{
		if(range < Vector2.Distance(position, target.position)) {
			throw new System.Exception("Can't not attack " + target.name + ". It is too far to attack!") ;
		}

		OnAttack (target);
		if (true == Hit (this, target)) {
			target.Damage (this, GetStatus ().attack);
		} else {
			target.OnDodge(this);
		}
	}
	
	public void Damage(Character attacker, int attack) {
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
		int effectiveDamage = (int)(attack * Mathf.Min ((float)attack/GetStatus ().defense, 1.0f));
		effectiveDamage += (int)(effectiveDamage * ((float)(UnityEngine.Random.Range (0, 100)) / 100.0f));
		health -= effectiveDamage;
		OnDamage (attacker, effectiveDamage);
		if (health <= 0) {
			Destroy();
		}
	}
	public ItemStack CreateItemStack(ItemData item, Position at) {
		ItemStack stack = new ItemStack ();
		stack.item = item;
		stack.count = 1;
		stack.SetPosition (at);
		return stack;
	}

	public virtual void OnAttack(Character target) {}
	public virtual void OnDamage(Character attacker, int damage) {}
	public virtual void OnDodge (Character attacker) {}
	public virtual void OnMove(Character.DirectionType direction) {}
	public virtual void OnEquipItem (ItemData item) {}
	public virtual void OnUnequipItem(ItemData item) {}
	public virtual void OnDropItem(ItemData item) {}
	public virtual void OnPickupItem(ItemData item) {}
	*/
}
