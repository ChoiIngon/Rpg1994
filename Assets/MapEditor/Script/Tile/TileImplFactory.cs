using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MapEditor {
	public abstract class TileImpl {
		public abstract string GetType();
		public abstract void SetText(Tile tile);
		public abstract void EditDialog ();
		public abstract JSONNode ToJSON(Tile tile);
		public abstract void FromJSON(Tile tile, JSONNode node);
		public string type {
			get {
				return GetType();
			}
		}
	}
	public class TileImplFactory : Util.Singleton<TileImplFactory> {
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