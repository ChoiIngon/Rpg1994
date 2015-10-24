using UnityEngine;
using System.Collections;

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
			tile.color = Util.HexToColor(sTreeColors[Random.Range (0, sTreeColors.Length)]);
		}
		public override void EditDialog(){
		}
	}
}
