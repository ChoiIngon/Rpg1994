using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace MapEditor {
	public class Tree : TileImpl {
		public static TileImpl Create() {
			return new Tree ();
		}
		
		public Tree() {
		}
		public override string GetType() {
			return "Tree";
		}
		public override void SetText (Tile tile) {
			string [] sTreeTexts = {"♠", "♣"};
			string [] sTreeColors = {"437C17", "347C2C", "254117", "387C44", "6CBB3C", "54C571"};
			tile.text = sTreeTexts [Random.Range (0, sTreeTexts.Length)];
			tile.color = Util.Color.HexToColor(sTreeColors[Random.Range (0, sTreeColors.Length)]);
		}
		public override void EditDialog(){
		}
		public override JSONNode ToJSON (Tile tile)
		{
			JSONNode node = new JSONClass ();
			node ["x"].AsInt = tile.position.x;
			node ["y"].AsInt = tile.position.y;
			node ["type"] = GetType ();
			node ["text"] = tile.text;
			node ["color"] = Util.Color.ColorToHex(tile.color);
			return node;
		}
		public override void FromJSON(Tile tile, JSONNode node)
		{
			tile.position.x = node ["x"].AsInt;
			tile.position.y = node ["y"].AsInt;
			tile.text = node ["text"];
			tile.color = Util.Color.HexToColor (node["color"]);
		}
	}
}
