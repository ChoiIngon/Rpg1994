using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map {
	public class MapView : MonoBehaviour {
		public Text text;
		public MapData map_data = null;
		// Use this for initialization
		void Start () {
			text = GetComponent<Text> ();
			MapInfo info = new MapInfo ();
			info.width = 50;
			info.height = 50;
			info.room_count = 9;
			map_data = info.CreateInstance ();
		}
		
		// Update is called once per frame
		void Update () {
			text.text = "";
			for (int y = 0; y < map_data.info.width; y++) {
				for (int x = 0; x < map_data.info.height; x++) {
					Tile tile = map_data.GetTile (x, y);
					if (0 == tile.group_id) {
						text.text += "#";
					} else {
						text.text += "·";
					}
				}

				text.text += "\n";
			}
		}
	}
}