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