using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor {
	public abstract class TileImpl {
		public abstract string GetType();
		public abstract void SetText(Tile tile);
		public abstract void EditDialog ();

		public string type {
			get {
				return GetType();
			}
		}
	}
	public class TileImplFactory : SingletonObject<TileImplFactory> {
		public delegate TileImpl Creator();
		private Dictionary<string, Creator> dictCreator = null;

		public TileImplFactory()
		{
			dictCreator = new Dictionary<string, Creator>();
		}
		public TileImpl Create(string type)
		{
			return dictCreator [type] ();
		}

		public void Register(string type, Creator creator) {
			dictCreator.Add (type, creator);
		}
	}
}