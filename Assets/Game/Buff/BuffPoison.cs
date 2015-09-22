using UnityEngine;
using System.Collections;

public class PoisonBuffInfo : BuffInfo {
	public RangeInt damage = new RangeInt ();
	public int turn;
	public override BuffData CreateInstance() {
		PoisonBuffData data = new PoisonBuffData ();
		data.info = this;
		data.expire = Game.Instance.currentTurn + turn;
		return data;
	}
}

public class PoisonBuffData : BuffData {
	public int expire;
	public override bool IsValid() {
		return Game.Instance.currentTurn < expire;
	}
	public override Character.StateData ApplyBuff (Character character) {
		int damage = ((PoisonBuffInfo)info).damage * -1;
		character.health.SetDelta (damage);
		Character.StateData state = new Character.StateData ();
		state.health += damage;
		return state;
	}
}