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
	public class Dialouge {
		public string speacker;
		public string script;
	};

	public string id;
	public string name;
	public State state = State.BeforeStart;
	public RewardInfo reward = new RewardInfo();
	public List<QuestStartCondition> startConditions = new List<QuestStartCondition> ();
	public List<QuestCompleteCondition> completeConditions = new List<QuestCompleteCondition> ();
	public List<Dialouge> startDialouges = new List<Dialouge> ();
	public List<Dialouge> completeDialouges = new List<Dialouge> ();

	public bool IsAvailable() {
		if (State.BeforeStart != state) {
			return false;
		}
		foreach (QuestStartCondition condition in startConditions) {
			if(false == condition.IsAvailable())
			{
				return false;
			}
		}
		return true;
	}

	public void Start()
	{
		if (false == IsAvailable ()) {
			return;
		}
		foreach (QuestCompleteCondition condition in completeConditions) {
			condition.Start ();
		}
		state = State.OnExecute;
		GameManager.Instance.player.quests.Add (id, this);
		QuestManager.Instance.OnStart (this);
	}

	public bool IsComplete() {
		if (State.OnExecute != state) {
			return false;
		}
		foreach (QuestCompleteCondition condition in completeConditions) {
			if(false == condition.IsComplete())
			{
				return false;
			}
		}
		state = State.BeforeStart;
		GameManager.Instance.player.inventory.gold += reward.gold;
		foreach (ItemInfo info in reward.items) {
			ItemData data = ItemManager.Instance.CreateInstance(info.id);
			GameManager.Instance.player.inventory.Put (data);
		}
		CompleteQuest complete = new CompleteQuest ();
		complete.id = id;

		QuestManager.Instance.completes.Add (id, complete);
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
	public Dictionary<string, CompleteQuest> completes = new Dictionary<string, CompleteQuest>();
	public Dictionary<string, QuestData> quests = new Dictionary<string, QuestData>();

	public void Init() {
		QuestData quest = new QuestData();
		quest.id = "quest_001";
		quest.name = "first quest";
		quest.startConditions.Add (new QuestStartCondition_Level () { level = 1 } );
		quest.startConditions.Add (new QuestStartCondition_Incomplete() { questID = "quest_001"});
		quest.startDialouges.Add (new QuestData.Dialouge() { speacker="촌장", script="어서 오세요. 용사님. 누군가가 이 마을에 찾아 온건 참 오랜만이군요. 요즘 들어 부쩍 마을 근처 몬스터들이 사람들을 공격하는 횟수가 늘었 답니다. 마을 주변에서 슬라임을 처치해 주시지 않겠습니까?"});

		quest.completeConditions.Add (new QuestCompleteCondition_KillMonster() {monsterID="monster_001", goalKillCount=1});
		quest.completeConditions.Add (new QuestCompleteCondition_MeetNpc() {npcID="npc_001"});
		quest.completeDialouges.Add (new QuestData.Dialouge() { speacker="촌장", script="good!!"});
		quest.reward.gold = new Util.RangeInt("100");
		quest.reward.items.Add(ItemManager.Instance.Find("shield_001"));
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
		LogView.Instance.Write ("[" + quest.name + "]");
		foreach (QuestData.Dialouge dialouge in quest.startDialouges) {
			LogView.Instance.Write (dialouge);
		}
	}
	public void OnComplete(QuestData quest)
	{
		foreach (QuestData.Dialouge dialouge in quest.completeDialouges) {
			LogView.Instance.Write (dialouge);
		}
		LogView.Instance.Write ("You get " + (int)quest.reward.exp + " exp");
		LogView.Instance.Write ("You get " + (int)quest.reward.gold + " gold");
		foreach (ItemInfo info in quest.reward.items) {
			LogView.Instance.Write ("You get " + info.name);
		}
	}
}


