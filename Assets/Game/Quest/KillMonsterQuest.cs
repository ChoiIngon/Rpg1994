using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillMonsterQuestInfo : QuestInfo {
}

public class KillMonsterQuestData : QuestData {
	public class KillCount {
		int current;
		int goal;
	};
	public string id;
	public string name;
	public Dictionary<string, KillCount> progress;

};

