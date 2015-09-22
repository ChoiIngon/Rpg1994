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
	public abstract bool IsStart ();
}

public class QuestData {
	public QuestInfo info;
//	public bool IsComplete() {
//	}
}

public class QuestManager : SingletonObject<QuestManager> {
	public Dictionary<string, string> completeQuests;
	public Dictionary<string, QuestInfo> currentQuests;

	public void Init() {
	}
	public void KillMonster(string monsterID) {
	}
}


public class KillMonsterQuestInfo : QuestData {
	public class KillCount {
		int current;
		int goal;
	};
	public string id;
	public string name;
	public Dictionary<string, KillCount> progress;
	public void KillMonster(string id) {
	}
};


