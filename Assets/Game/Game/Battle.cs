using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack {
	public enum AttackType {
		Normal,
		Critical,
		CounterAttack
	}
	public class AttackResult {
		public AttackType type;
		public int damage;
	}

	public List<AttackResult> result = new List<AttackResult> ();
	public Attack(Character attacker, Character defender)
	{
		AttackResult attackResult = new AttackResult ();
		attackResult.damage = attacker.GetAttack ();
		attackResult.type = AttackType.Normal;

		attackResult.damage -= defender.GetDefense ();
		if (0 >= attackResult.damage) {
			attackResult.damage = 1;
		}

		defender.health.SetDelta (attackResult.damage * -1);
		result.Add (attackResult);
		attacker.GetState ();
		defender.GetState ();
	}
}
