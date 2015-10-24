using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour{
	private static T self_;
	private static object lock_ = new object();

	public static T Instance {
		get {
			lock (lock_) {
				if (null == self_) {
					self_ = (T)FindObjectOfType (typeof(T));
					if (null == self_) {
						GameObject singleton = new GameObject ();
						self_ = singleton.AddComponent<T> ();
						singleton.name = typeof(T).ToString ();
						DontDestroyOnLoad (singleton);
					}

				}
			}
			return self_;
		}
	}
}

public class SingletonObject<T> where T : class, new() {
	private static T self_ = null;
	private static object lock_ = new object();
	
	public static T Instance {
		get;
		private set;
	}

	static SingletonObject() {
		if (null == SingletonObject<T>.Instance) {
			SingletonObject<T>.Instance = new T ();
		}
	}
}