using UnityEngine;
using System.Collections;

public class StatusBarInt {
	public int max;
	public int current;
	public int amount;
	public int time;
	public int updateTime;

	public int GetValue() {
		int currentTime = Game.Instance.currentTurn;
		if (max > current) {
			int deltaTime = currentTime - updateTime;
			if (deltaTime >= time) {
				current += deltaTime / time * amount;
				if (max < current) {
					current = max;
				}
				updateTime = currentTime;
			}
		} else {
			updateTime = currentTime;
		}
		return current;
	}
	public int SetDelta(int delta) {
		GetValue ();
		current += delta;
		if (0 > current) {
			current = 0;
		}
		if (max < current) {
			current = max;
		}
		return current;
	}

	public static implicit operator int (StatusBarInt value) {
		return value.GetValue();
	}

	public static StatusBarInt operator + (StatusBarInt value, int delta)
	{
		value.SetDelta(delta);
		return value;
	}
}
