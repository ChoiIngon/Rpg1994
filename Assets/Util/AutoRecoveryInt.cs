using System;
using System.Collections;

public abstract class ITimerImpl
{
	public ITimerImpl() {}
	public abstract int GetTime ();
	public abstract void SetTime (int currentTime);
	public abstract void NextTime ();
};

public class TurnCounter : ITimerImpl {
	private int turn = 1;
	public TurnCounter() {
	}
	public override int GetTime() {
		return turn;
	}
	public override void SetTime(int currentTime)	{
		turn = currentTime;
	}
	public override void NextTime() {
		turn++;
	}
};

public class UnixTimestamp : ITimerImpl {
	private int time;
	public UnixTimestamp() {
	}
	private int GetUnixtime() {
		return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
	}
	public override int GetTime() {
		return time;
	}
	public override void SetTime(int currentTime) {
		time = currentTime;
	}
	public override void NextTime() {
		time = GetUnixtime ();
	}
};

public class Timer<T> : SingletonObject<Timer<T>> where T : ITimerImpl, new() {
	ITimerImpl impl = null;
	public Timer() {
		impl = new T ();
	}
	public int GetTime() {
		return impl.GetTime();
	}
	public void SetTime(int currentTime) {
		impl.SetTime (currentTime);
	}
	public void NextTime() {
		impl.NextTime ();
	}
}

public class AutoRecoveryInt<T> where T : ITimerImpl, new() {
	public int max;
	public int value;
	public int recovery;
	public int interval;
	public int time;

	public int GetValue() {
		int currentTime = Timer<T>.Instance.GetTime ();
		if (max > value) {
			int deltaTime = currentTime - time;
			int recoveryCount = deltaTime / interval;
			value = Math.Min (recoveryCount * recovery + value, max);
			if(value == max) {
				time = currentTime;
			}
			else {
				time = recoveryCount * interval + time;
			}
		} else {
			time = currentTime;
		}
		return value;
	}
	public int SetDelta(int delta) {
		GetValue ();
		value += delta;
		if (0 > value) {
			value = 0;
		}
		if (max < value) {
			value = max;
		}
		return value;
	}

	public static implicit operator int (AutoRecoveryInt<T> value) {
		return value.GetValue();
	}

	public static AutoRecoveryInt<T> operator + (AutoRecoveryInt<T> value, int delta)
	{
		value.SetDelta(delta);
		return value;
	}
	public static AutoRecoveryInt<T> operator - (AutoRecoveryInt<T> value, int delta)
	{
		value.SetDelta (delta);
		return value;
	}
}
