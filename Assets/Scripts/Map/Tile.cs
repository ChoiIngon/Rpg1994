using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Map {
	public class Tile {
		public Position position = new Position();
		public bool visit = false;
		public bool visible = false;
		public List<Object> objects = new List<Object>();
		public int group_id = 0;

		public void AddObject(Object obj) {
			objects.Add (obj);
		}

		public void RemoveObject(Object obj) {
			objects.Remove (obj);
		}

		public T FindObject<T>() where T : Object {
			foreach (var o in objects) {
				if(o is T) {
					return (T)o;
				}
			}
			return null;
		}
	}
}