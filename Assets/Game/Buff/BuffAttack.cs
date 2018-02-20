using UnityEngine;
using System.Collections;

public class AttackBuffInfo : BuffInfo {
	public int turn;
	public int attack;
	public override BuffData CreateInstance() {
		AttackBuffData data = new AttackBuffData ();
		data.info = this;
		data.expire = GameManager.Instance.currentTurn + turn;
		return data;
	}
}

public class AttackBuffData : BuffData {
	public int expire;
	public override bool IsValid() {
		return GameManager.Instance.currentTurn < expire;
	}
	/*
	public override Character.Status ApplyBuff (Character character) {
		Character.Status status = new Character.Status ();
		status.attack += ((AttackBuffInfo)info).attack;
		return status;
	}
	*/
}

