using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class MonsterInfo {
	public string id;
	public string name;
	public string description;
	public RangeInt attack = new RangeInt();
	public RangeInt speed = new RangeInt();
	public RangeInt defense = new RangeInt();
	public RangeInt maxHealth = new RangeInt ();
	public AutoRecoveryInt<TurnCounter> health = new AutoRecoveryInt<TurnCounter> ();

	public ItemInfo[] items = new ItemInfo[(int)Character.EquipPart.Max];
	public RangeInt gold = new RangeInt();
	public MonsterData CreateInstance() {
		MonsterData data = new MonsterData ();
		data.info = this;
		data.name = name;
		data.attack = attack;
		data.speed = speed;
		data.defense = defense;
		data.health = new AutoRecoveryInt<TurnCounter>();
		data.health.recovery = health.recovery;
		data.health.interval = health.interval;
		data.health.value = maxHealth;
		data.health.max = maxHealth;
	
		for (int i=0; i<items.Length; i++) {
			if(null != items[i])
			{
				data.EquipItem(items[i].CreateInstance() as EquipmentItemData, (Character.EquipPart)i);
			}
		}
		return data;
	}
}