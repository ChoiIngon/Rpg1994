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
	public StatusBarInt health = new StatusBarInt ();

	public ItemInfo[] items = new ItemInfo[(int)Character.EquipPart.Max];
	public RangeInt gold = new RangeInt();
	public MonsterData CreateInstance() {
		MonsterData data = new MonsterData ();
		data.info = this;
		data.name = name;
		data.attack = attack;
		data.speed = speed;
		data.defense = defense;
		data.health = new StatusBarInt();
		data.health.amount = health.amount;
		data.health.time = health.time;
		data.health.current = maxHealth;
		data.health.max = maxHealth;
		data.health.current = data.health.max;
		for (int i=0; i<items.Length; i++) {
			if(null != items[i])
			{
				data.EquipItem(items[i].CreateInstance() as EquipmentItemData, (Character.EquipPart)i);
			}
		}
		return data;
	}
}