using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestInfo {
	public enum Type {
		KillMonster
	}

	public class RewardInfo {
		public int gold = 0;
		public int exp = 0;
		public List<ItemInfo> items = new List<ItemInfo> ();
	}

	public string id;
	public string name;
	public RewardInfo reward;
}

public class QuestData {
	public QuestInfo info;
//	public bool IsComplete() {
//	}
}

public class QuestManager : Util.Singleton<QuestManager> {
	public Dictionary<string, string> completeQuests;
	public Dictionary<string, QuestInfo> currentQuests;

	public void Init() {
	}
	public void KillMonster(string monsterID) {
	}
}


