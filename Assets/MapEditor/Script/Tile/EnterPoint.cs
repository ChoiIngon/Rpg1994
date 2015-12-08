using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace MapEditor {
	public class EnterPoint : TileImpl {
		public string name;
		public string description;

		public static TileImpl Create() {
			return new EnterPoint ();
		}
		
		public EnterPoint() {
			name = "";
			description = "";
			Debug.Log ("init enter point name");
			Debug.Log ("init enter point description");
		}
		public override string GetType() {
			return "EnterPoint";
		}
		public override void SetText (Tile tile) {
			tile.text = "E";
			tile.color = Color.green;
		}
		public override void EditDialog(){
			EnterPointDialog dialog = EnterPointDialog.Instance;
			dialog.gameObject.SetActive (true);
			dialog.name = name;
			dialog.description = description;
			dialog.tile = this;
		}
		public override JSONNode ToJSON (Tile tile)
		{
			JSONNode node = new JSONClass (); 
			node ["x"].AsInt = tile.position.x;
			node ["y"].AsInt = tile.position.y;
			node ["type"] = GetType ();
			node ["name"] = name;
			node ["description"] = description;
			return node;
		}
		public override void FromJSON(Tile tile, JSONNode node)
		{
			tile.position.x = node ["x"].AsInt;
			tile.position.y = node ["y"].AsInt;
			tile.text = "E";
			tile.color = Color.green;
			name = node ["name"];
			description = node ["description"];
		}
	}
}
