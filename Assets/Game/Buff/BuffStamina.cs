using UnityEngine;
using System.Collections;

public class StaminaBuffInfo : BuffInfo {
	public int amount;
	public override BuffData CreateInstance() {
		StaminaBuffData data = new StaminaBuffData ();
		data.info = this;
		data.count = 1;
		return data;
	}
}

public class StaminaBuffData : BuffData {
	public int count;
	public override bool IsValid() {
		return count > 0;
	}
	/*
	public override Character.Status ApplyBuff(Character character)
	{
		int amount = ((StaminaBuffInfo)info).amount;
		character.stamina += amount;
		Character.Status status = new Character.Status ();
		status.stamina += amount;
		count--;
		return status;
	}
	*/
}

