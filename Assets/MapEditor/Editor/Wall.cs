using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MapEditor {
	public class Wall : MonoBehaviour {
		public static char [] block = new char[]{'─', '│', '┌', '┐', '┘', '└', '├', '┤', '┬', '┴', '┼'};
		public global::Tile.Type type {
			get {
				return global::Tile.Type.Wall;
			}
		}
		// Use this for initialization
		void Start () {
			gameObject.AddComponent<Text> ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

//		public Text GetTileText(int x, int y) {
//			Game.Instance.map.tiles;
//		}
	}
}