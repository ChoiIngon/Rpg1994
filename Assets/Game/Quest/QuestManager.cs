using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestData {
	public enum State {
		BeforeStart,
		OnExecute,
		Complete,
		Max
	};
	public class DialougeInfo {
		public Character speacker;
		public string script;
		public string image;
	};

	public class RewardInfo {
		public int gold = 0;
		public int exp = 0;
		public List<ItemInfo> items = new List<ItemInfo> ();
	}

	public string id;
	public string name;
	public State state = State.BeforeStart;
	public RewardInfo reward = new RewardInfo();
	public List<QuestStartCondition> triggers = new List<QuestStartCondition> ();
	public List<QuestCompleteCondition> conditions = new List<QuestCompleteCondition> ();

	public bool IsAvailable() {
		if (State.BeforeStart != state) {
			return false;
		}
		foreach (QuestStartCondition trigger in triggers) {
			if(false == trigger.IsAvailable())
			{
				return false;
			}
		}
		return true;
	}

	public void Start()
	{
		foreach (QuestCompleteCondition condition in conditions) {
			condition.Init ();
		}
		state = State.OnExecute;
		QuestManager.Instance.OnStart (this);
	}
	public bool IsComplete() {
		if (State.OnExecute != state) {
			return false;
		}
		foreach (QuestCompleteCondition condition in conditions) {
			if(false == condition.IsComplete())
			{
				return false;
			}
		}

		state = State.Complete;
		GameManager.Instance.player.inventory.gold += reward.gold;
		foreach (ItemInfo info in reward.items) {
			ItemData data = ItemManager.Instance.CreateInstance(info.id);
			GameManager.Instance.player.inventory.Put (data);
		}

		QuestManager.Instance.OnComplete (this);
		return true;
	}
}

public class CompleteQuest {
	public string id;
	public int date;
	public int count;
};

public class QuestManager : Util.Singleton<QuestManager> {
	public Dictionary<string, CompleteQuest> complete = new Dictionary<string, CompleteQuest>();
	public Dictionary<string, QuestData> quests = new Dictionary<string, QuestData>();

	public void Init() {
		QuestData quest = new QuestData();
		quest.id = "quest_001";
		quest.name = "first quest";
		quest.reward.gold = 100;
		quest.reward.items.Add(ItemManager.Instance.Find("shield_001"));
		quest.triggers.Add (new QuestStartCondition_Level ());
		quest.conditions.Add (new QuestCompleteCondition_KillMonster{monsterID="monster_001", goalKillCount=1, currentKillCount=0});
		quests.Add (quest.id, quest);
	}
	public QuestData Find(string questID)
	{ 
		return quests.ContainsKey (questID) ? quests [questID] : null;
	}

	public delegate void TriggerKillMonster(string monsterID);

	public TriggerKillMonster triggerKillMonster;

	public void OnStart (QuestData quest)
	{
		LogView.Instance.Write ("quest starts");
	}
	public void OnComplete(QuestData quest)
	{
		LogView.Instance.Write ("quest completed");
	}
}


