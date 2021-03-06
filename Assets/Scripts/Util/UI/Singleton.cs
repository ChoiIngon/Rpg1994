﻿using UnityEngine;
using System.Collections;

namespace Util { 
	namespace UI {
		public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
			private static T self_;
			public static T Instance {
				get {
					if (null == self_) {
						self_ = (T)FindObjectOfType (typeof(T));
						if (null == self_) {
							GameObject singleton = new GameObject ();
							singleton.name = typeof(T).ToString ();
							self_ = singleton.AddComponent<T> ();
							if(null == self_)
							{
								throw new System.Exception("singleton object is null");
							}
							DontDestroyOnLoad (singleton);
						}
						Debug.Log (self_.name + " is created");
					}
					return self_;
				}
			}
		
			void Awake()
			{
				if (null == self_) {
					self_ = this as T;
				}
			}
		}
	}
}
