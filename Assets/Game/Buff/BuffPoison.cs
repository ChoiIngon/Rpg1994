using UnityEngine;
using System.Collections;

public class PoisonBuffInfo : BuffInfo {
	public Util.RangeInt damage = new Util.RangeInt ();
	public int turn;
	public override BuffData CreateInstance() {
		PoisonBuffData data = new PoisonBuffData ();
		data.info = this;
		data.current = GameManager.Instance.currentTurn;
		data.expire = GameManager.Instance.currentTurn + turn + 1;
		return data;
	}
}

public class PoisonBuffData : BuffData {
	public int expire;
	public int current;
	public override bool IsValid() {
		return GameManager.Instance.currentTurn < expire;
	}
	public override Character.Status ApplyBuff (Character character) {
		Character.Status status = new Character.Status ();
		if (current < GameManager.Instance.currentTurn) {
			int damage = ((PoisonBuffInfo)info).damage;
			character.health -= damage;
			status.health -= damage;
			character.OnDamage(character, damage);
			current = GameManager.Instance.currentTurn;
		}
		return status;
	}
}