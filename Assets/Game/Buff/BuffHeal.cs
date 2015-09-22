using UnityEngine;
using System.Collections;

public class HealBuffInfo : BuffInfo {
	public int amount;
	public override BuffData CreateInstance() {
		HealBuffData data = new HealBuffData ();
		data.info = this;
		data.count = 1;
		return data;
	}
}

public class HealBuffData : BuffData {
	public int count;
	public override bool IsValid() {
		return count > 0;
	}
	public override Character.StateData ApplyBuff(Character character)
	{
		int amount = ((HealBuffInfo)info).amount;
		character.health.SetDelta (amount);
		Character.StateData state = new Character.StateData ();
		state.health += amount;
		count--;
		return state;
	}
}

