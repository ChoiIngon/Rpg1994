using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestCompleteCondition {
	public enum Type {
		KillMonster,
		MeetNpc,
		GatherItem,
		Max
	}

	public abstract void Start();
	public abstract bool IsComplete();
	public QuestCompleteCondition.Type type {
		protected set;
		get;
	}
}

public class QuestCompleteCondition_KillMonster : QuestCompleteCondition {
	public string monsterID;
	public int goalKillCount;
	public int currentKillCount;

	public QuestCompleteCondition_KillMonster() {
		type = QuestCompleteCondition.Type.KillMonster;
	}

	public void Trigger(string monsterID) {
		if (this.monsterID == monsterID) {
			currentKillCount++;
		}
	}

	public override void Start() {
		currentKillCount = 0;
		QuestManager.Instance.triggerKillMonster += this.Trigger;
	}

	public override bool IsComplete() {
		if (currentKillCount >= goalKillCount) {
			QuestManager.Instance.triggerKillMonster -= this.Trigger;
			return true;
		}
		return false;
	}
}

public class QuestCompleteCondition_MeetNpc : QuestCompleteCondition {
	public string npcID;
	
	public QuestCompleteCondition_MeetNpc() {
		type = QuestCompleteCondition.Type.MeetNpc;
	}
	
	public override void Start() {
	}
	
	public override bool IsComplete() {
		/*
		Object obj = Player.Instance.target;
		if(null != obj && Object.Category.NPC == obj.category) {
			Npc npc = (Npc)obj;
			if(npcID == npc.id)
			{
				return true;
			}
		}
		*/
		return false;
	}
}
