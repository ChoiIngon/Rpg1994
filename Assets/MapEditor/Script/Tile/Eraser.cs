using UnityEngine;
using System.Collections;

namespace MapEditor {
	public class Eraser : TileImpl {
		public static TileImpl Create() {
			return new Eraser ();
		}
		
		public Eraser() {
		}
		public override string GetType() {
			return "";
		}
		public override void SetText (Tile tile) {
			tile.text = "";
			tile.color = Color.white;
		}
		public override void EditDialog(){
		}
	}
}
