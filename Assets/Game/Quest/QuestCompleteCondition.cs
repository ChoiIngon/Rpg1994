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

	public abstract void Init();
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
		QuestManager.Instance.triggerKillMonster = this.Trigger;
	}

	public void Trigger(string monsterID) {
		if (this.monsterID == monsterID) {
			currentKillCount++;
		}
	}

	public override void Init() {
		currentKillCount = 0;
	}

	public override bool IsComplete() {
		if (currentKillCount >= goalKillCount) {
			return true;
		}
		return false;
	}
}