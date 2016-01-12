using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

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
		Player.Instance.quests.Add (id, this);
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
		Player.Instance.quests.Remove (id);
		state = State.BeforeStart;
		Player.Instance.inventory.gold += reward.gold;
		foreach (ItemInfo info in reward.items) {
			ItemData data = ItemManager.Instance.CreateInstance(info.id);
			Player.Instance.inventory.Put (data);
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

	private delegate QuestStartCondition CreateStartConditionInstance(JSONNode attr);
	private delegate QuestCompleteCondition CreateCompleteConditionInstance (JSONNode attr);
	public void Init() {
		completes = new Dictionary<string, CompleteQuest>();
		quests = new Dictionary<string, QuestData>();


		Dictionary<string, CreateStartConditionInstance> startConditions = new Dictionary<string, CreateStartConditionInstance>();
		startConditions ["npc_id"] = (JSONNode attr) => { 
			QuestStartCondition_MeetNpc condition = new QuestStartCondition_MeetNpc ();
			condition.npcID = attr;
			return condition;
		};
		startConditions ["level"] = (JSONNode attr) => { 
			QuestStartCondition_Level condition = new QuestStartCondition_Level ();
			condition.level = attr.AsInt;
			return condition;
		};
		startConditions ["complete_quest"] = (JSONNode attr) => { 
			QuestStartCondition_Complete condition = new QuestStartCondition_Complete ();
			condition.questID = attr;
			return condition;
		};
		startConditions ["incomplete_quest"] = (JSONNode attr) => { 
			QuestStartCondition_Incomplete condition = new QuestStartCondition_Incomplete ();
			condition.questID = attr;
			return condition;
		};

		Dictionary<string, CreateCompleteConditionInstance> completeConditions = new Dictionary<string, CreateCompleteConditionInstance> ();
		completeConditions ["kill_monster"] = (JSONNode attr) => {
			QuestCompleteCondition_KillMonster condition = new QuestCompleteCondition_KillMonster();
			condition.monsterID = attr["monster_id"];
			condition.goalKillCount = attr["count"].AsInt;
			return condition;
		};
		completeConditions ["meet_npc"] = (JSONNode attr) => {
			QuestCompleteCondition_MeetNpc condition = new QuestCompleteCondition_MeetNpc();
			condition.npcID = attr["npc_id"];
			return condition;
		};
		TextAsset resource = Resources.Load("Config/QuestInfo") as TextAsset;
		JSONNode root = JSON.Parse (resource.text);
		JSONNode jQuests = root ["quest"];
		for (int i=0; i<jQuests.Count; i++) {
			JSONNode jQuest = jQuests [i];
			QuestData data = new QuestData ();
			data.id = jQuest["id"];
			data.name = jQuest["name"];

			JSONNode jStartConditions = jQuest["start_condition"];
			for(int j=0; j<jStartConditions.Count; j++)
			{
				JSONClass jStartCondition = jStartConditions[j].AsObject;
				foreach(KeyValuePair<string, JSONNode> pair in jStartCondition)
				{
					data.startConditions.Add (startConditions[pair.Key](pair.Value));
				}
			}

			JSONNode jStartDialouges = jQuest["start_dialogue"];
			for(int j=0; j<jStartDialouges.Count; j++)
			{
				JSONNode jStartDialouge = jStartDialouges[j];
				data.startDialouges.Add (new QuestData.Dialouge() { speacker=jStartDialouge["speaker"], script=jStartDialouge["script"]});
			}
			JSONNode jCompleteConditions = jQuest["complete_condition"];
			for(int j=0; j<jCompleteConditions.Count; j++)
			{
				JSONNode jCompleteCondition = jCompleteConditions[j];
				data.completeConditions.Add (completeConditions[jCompleteCondition["type"]](jCompleteCondition));
			}
			JSONNode jCompleteDialouges = jQuest["complete_dialogue"];
			for(int j=0; j<jCompleteDialouges.Count; j++)
			{
				JSONNode jCompleteDialouge = jCompleteDialouges[j];
				data.completeDialouges.Add (new QuestData.Dialouge() { speacker=jCompleteDialouge["speaker"], script=jCompleteDialouge["script"]});
			}
			JSONNode jRewards = jQuest["reward"];
			for(int j=0; j<jRewards.Count; j++)
			{
				JSONNode jReward = jRewards[j];
				string sGold = jReward["gold"];
				if(null != sGold)
				{
					data.reward.gold.SetValue(sGold);
				}
				string sItem = jReward["item"];
				if(null != sItem)
				{
					data.reward.items.Add(ItemManager.Instance.Find(sItem));
				}
			}
			quests.Add (data.id, data);
		}
		/*
		QuestData quest = new QuestData();
		quest.id = "quest_002";
		quest.name = "first quest";
		quest.startConditions.Add (new QuestStartCondition_Level () { level = 1 } );
		quest.startConditions.Add (new QuestStartCondition_MeetNpc() { npcID = "npc_001" });
		quest.startConditions.Add (new QuestStartCondition_Incomplete() { questID = "quest_001"});
		quest.startDialouges.Add (new QuestData.Dialouge() { speacker="촌장", script="어서 오세요. 용사님. 누군가가 이 마을에 찾아 온건 참 오랜만이군요. 요즘 들어 부쩍 마을 근처 몬스터들이 사람들을 공격하는 횟수가 늘었 답니다. 마을 주변에서 슬라임을 처치해 주시지 않겠습니까?"});

		quest.completeConditions.Add (new QuestCompleteCondition_KillMonster() {monsterID="monster_001", goalKillCount=1});
		quest.completeConditions.Add (new QuestCompleteCondition_MeetNpc() {npcID="npc_001"});
		quest.completeDialouges.Add (new QuestData.Dialouge() { speacker="촌장", script="good!!"});
		quest.reward.gold = new Util.RangeInt("100");
		quest.reward.items.Add(ItemManager.Instance.Find("shield_001"));
		quests.Add (quest.id, quest);
		*/
	}
	
	public QuestData Find(string questID)
	{ 
		return quests.ContainsKey (questID) ? quests [questID] : null;
	}

	public delegate void TriggerKillMonster(string monsterID);

	public TriggerKillMonster triggerKillMonster;

	public QuestData GetAvailableQuest()
	{
		foreach(var v in quests)
		{
			QuestData quest = v.Value;
			if(true == quest.IsAvailable())
			{
				return quest;
			}
		}
		return null;
	}

	public QuestData GetCompleteQuest()
	{
		List<KeyValuePair<string, QuestData>> list = this.quests.ToList ();
		foreach (KeyValuePair<string, QuestData> pair in list)
		{
			if(true == pair.Value.IsComplete())
			{
				return pair.Value;
			}
		}
		return null;
	}

	public void OnStart (QuestData quest)
	{
		LogView.Instance.Write ("[" + quest.name + "] starts");
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


