using UnityEngine;
using System;
using System.Collections;

namespace Util {
	public abstract class ITimerImpl
	{
		public ITimerImpl() {}
		public abstract int GetTime ();
		public abstract void SetTime (int currentTime);
		public abstract void NextTime ();
	};

	public class TurnCounter : ITimerImpl {
		private int turn = 0;
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

	public class Timer<T> : Singleton<Timer<T>> where T : ITimerImpl, new() {
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
	};
}