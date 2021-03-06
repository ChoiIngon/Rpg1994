﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class MonsterManager : Util.Singleton<MonsterManager> {
	private Dictionary<string, MonsterInfo> dictInfo = null;

	public MonsterManager() {
	}
	public void Init() {
		/*
		dictInfo = new Dictionary<string, MonsterInfo>();
		TextAsset resource = Resources.Load("Config/MonsterInfo") as TextAsset;
		JSONNode root = JSON.Parse (resource.text);
		JSONNode jMonsterInfos = root ["monster"];
		for (int i=0; i<jMonsterInfos.Count; i++) {
			JSONNode jMonsterInfo = jMonsterInfos [i];
			MonsterInfo info = new MonsterInfo ();
			info.id = jMonsterInfo ["id"];
			info.name = jMonsterInfo ["name"];
			info.description = jMonsterInfo ["description"];
			info.maxHealth.SetValue(jMonsterInfo["health"]["max"]);
			info.health.recovery = jMonsterInfo["health"]["amount"].AsInt;
			info.health.interval = jMonsterInfo["health"]["time"].AsInt;
			info.attack.SetValue(jMonsterInfo["attack"]);
			info.speed.SetValue(jMonsterInfo["speed"]);
			info.defense.SetValue(jMonsterInfo["defense"]);

			info.equipments = InitItem (jMonsterInfo);

			JSONNode rewardInfos = jMonsterInfo["reward"];
			for(int j=0; j<rewardInfos.Count; j++)
			{
				JSONNode rewardInfo = rewardInfos[j];
				string rewardValue = rewardInfo["item"];
				if(null != rewardValue)
				{
					info.reward.items.Add (ItemManager.Instance.Find (rewardValue));
				}
				rewardValue = rewardInfo["gold"];
				if(null != rewardValue)
				{
					info.reward.gold.SetValue(rewardValue);
				}
				rewardValue = rewardInfo["exp"];
				if(null != rewardValue)
				{
					info.reward.exp.SetValue(rewardValue);
				}
			}
			dictInfo.Add (info.id, info);
		}
		Debug.Log ("init complete MonsterManager");
		*/
	}
	/*
	ItemInfo[] InitItem(JSONNode jInfo)
	{
		ItemInfo[] items = new ItemInfo[(int)Character.EquipPart.Max];
		string weapon = jInfo["weapon"];
		if(null != weapon)
		{
			items[(int)Character.EquipPart.Weapon] = ItemManager.Instance.Find (weapon) as WeaponItemInfo;
		}
		return items;
	}
	*/
	public MonsterInfo Find(string id) {
		return dictInfo [id];
	}
	public MonsterData CreateInstance(string id) {
		MonsterData monster = null;// = dictInfo [id].CreateInstance ();
		return monster;
	}
}
