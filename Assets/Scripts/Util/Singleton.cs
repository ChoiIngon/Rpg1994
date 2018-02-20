using UnityEngine;
using System.Collections;

namespace Util {
	public class Singleton<T> where T : class, new() {
		private static T self_;

		public static T Instance {
			get;
			private set;
		}

		static Singleton() {
			if (null == Singleton<T>.Instance) {
				Singleton<T>.Instance = new T ();
			}
		}
	}
}