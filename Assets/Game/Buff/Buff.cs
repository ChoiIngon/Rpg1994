using UnityEngine;
using System.Collections;

public abstract class BuffInfo {
	public string name;
	public abstract BuffData CreateInstance();
}

public abstract class BuffData {
	public BuffInfo info;
	public abstract bool IsValid ();
	public abstract Character.Status ApplyBuff (Character character);
}
