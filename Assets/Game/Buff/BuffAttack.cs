using UnityEngine;
using System.Collections;

public class AttackBuffInfo : BuffInfo {
	public int turn;
	public int attack;
	public override BuffData CreateInstance() {
		AttackBuffData data = new AttackBuffData ();
		data.info = this;
		data.expire = Game.Instance.currentTurn + turn;
		return data;
	}
}

public class AttackBuffData : BuffData {
	public int expire;
	public override bool IsValid() {
		return Game.Instance.currentTurn < expire;
	}
	public override Character.StateData ApplyBuff (Character character) {
		Character.StateData state = new Character.StateData ();
		state.attack += ((AttackBuffInfo)info).attack;
		return state;
	}
}

