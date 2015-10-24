using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class MonsterManager : SingletonObject<MonsterManager> {
	private int sequence;
	private Dictionary<string, MonsterInfo> dictInfo = null;
	public Dictionary<int, MonsterData> monsters = null;
	public MonsterManager() {
		sequence = 1;
		dictInfo = new Dictionary<string, MonsterInfo>();
		monsters = new Dictionary<int, MonsterData>();
		
		Init ();
	}
	private void Init() {
		TextAsset resource = Resources.Load ("MonsterInfo") as TextAsset;
		JsonData root = JsonMapper.ToObject (resource.text);
		JsonData monsterInfos = root ["monster"];
		for (int i=0; i<monsterInfos.Count; i++) {
			JsonData jsonInfo = monsterInfos [i];
			MonsterInfo info = new MonsterInfo();
			info.id = (string)jsonInfo["id"];
			info.name = (string)jsonInfo["name"];
			info.description = (string)jsonInfo["description"];
			info.maxHealth.SetValue((string)jsonInfo["health"]["max"]);
			info.health.recovery = (int)jsonInfo["health"]["amount"];
			info.health.interval = (int)jsonInfo["health"]["time"];
			info.attack.SetValue((string)jsonInfo["attack"]);
			info.speed.SetValue((string)jsonInfo["speed"]);
			info.defense.SetValue((string)jsonInfo["defense"]);
			if("" != (string)jsonInfo["weapon"])
			{
				info.items[(int)Character.EquipPart.Weapon] = ItemManager.Instance.Find ((string)jsonInfo["weapon"]) as WeaponItemInfo;
			}
			dictInfo.Add (info.id, info);
		}
	}
	public MonsterInfo Find(string id) {
		return dictInfo [id];
	}
	public MonsterData CreateInstance(string id) {
		MonsterData monster = dictInfo [id].CreateInstance ();
		monster.seq = sequence++;
		monsters.Add (monster.seq, monster);
		return monster;
	}
	public void Remove(int seq) {
		monsters.Remove (seq);
	}
}
