using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace MapEditor {
	public class ExitPoint : TileImpl {

		public string nextMap;
		public string enterPoint;

		public static TileImpl Create() {
			return new ExitPoint ();
		}
		
		public ExitPoint() {
		}
		public override string GetType() {
			return "ExitPoint";
		}
		public override void SetText (Tile tile) {
			tile.text = "E";
			tile.color = Color.red;
		}
		public override void EditDialog(){
			
		}
		public override JSONNode ToJSON (Tile tile)
		{
			JSONNode node = new JSONClass (); 
			node ["x"].AsInt = tile.position.x;
			node ["y"].AsInt = tile.position.y;
			node ["type"] = GetType ();
			node ["next_map"] = nextMap;
			node ["enter_point"] = enterPoint;
			return node;
		}
		public override void FromJSON(Tile tile, JSONNode node)
		{
			tile.position.x = node ["x"].AsInt;
			tile.position.y = node ["y"].AsInt;
			tile.text = "E";
			tile.color = Color.red;
			nextMap = node ["next_map"];
			enterPoint = node ["enter_point"];
		}
	}
}
