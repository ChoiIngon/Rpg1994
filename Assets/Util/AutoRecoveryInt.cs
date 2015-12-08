using System;
using System.Collections;

namespace Util {
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
}