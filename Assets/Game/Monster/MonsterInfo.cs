using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class MonsterInfo {
	public class RewardInfo {
		public Util.RangeInt gold = new Util.RangeInt();
		public List<ItemInfo> items = new List<ItemInfo> ();
	}
	public string id;
	public string name;
	public string description;
	public Util.RangeInt attack = new Util.RangeInt();
	public Util.RangeInt speed = new Util.RangeInt();
	public Util.RangeInt defense = new Util.RangeInt();
	public Util.RangeInt maxHealth = new Util.RangeInt ();
	public Util.AutoRecoveryInt<Util.TurnCounter> health = new Util.AutoRecoveryInt<Util.TurnCounter> ();
	public RewardInfo reward;
	public ItemInfo[] items = new ItemInfo[(int)Character.EquipPart.Max];

	public MonsterData CreateInstance() {
		MonsterData data = new MonsterData ();
		data.info = this;
		data.name = name;
		data.attack = attack;
		data.speed = speed;
		data.defense = defense;
		data.health = new Util.AutoRecoveryInt<Util.TurnCounter>();
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