using UnityEngine;
using System.Collections;

public class PoisonBuffInfo : BuffInfo {
	public Util.RangeInt damage = new Util.RangeInt ();
	public int turn;
	public override BuffData CreateInstance() {
		PoisonBuffData data = new PoisonBuffData ();
		data.info = this;
		data.expire = GameManager.Instance.currentTurn + turn;
		return data;
	}
}

public class PoisonBuffData : BuffData {
	public int expire;
	public override bool IsValid() {
		return GameManager.Instance.currentTurn < expire;
	}
	public override Character.Status ApplyBuff (Character character) {
		int damage = ((PoisonBuffInfo)info).damage * -1;
		character.health.SetDelta (damage);
		Character.Status status = new Character.Status ();
		status.health += damage;
		return status;
	}
}