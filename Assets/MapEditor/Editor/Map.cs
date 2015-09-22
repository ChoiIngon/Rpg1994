using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MapEditor {
	public class Map : MonoBehaviour {
		public class MapData
		{
			public class Size {
				public int width;
				public int height;
			}
			public class Tile
			{
				public int x;
				public int y;
				public string type;
				public string text;
				public string color;
			}
			public string name;
			public Size size = new Size ();
			public List<Tile> tiles = new List<Tile> ();
		}

		private const int TILE_SIZE = 20;
		MapData data = new MapData();
		public int width;
		public int height;
		public MapEditor.Tile tilePref;
		public string mapName;

		// Use this for initialization
		void Start () {
			Init (width, height);
		}

		public void Save(string path) {
			MapData jd = new MapData ();
			jd.name = this.mapName;
			jd.size.width = this.width;
			jd.size.height = this.height;

			Transform contents = transform.FindChild ("Contents");
			if (null == contents) {
				throw new System.Exception ("can't find contents object");
			}
			for(int y=0; y<height; y++) {
				for(int x=0; x<width; x++) {
			
					Transform tileTransform = contents.GetChild(x + y * width);
					Tile tileScript = tileTransform.GetComponent<Tile>();
					if (null == tileScript) {
						throw new System.Exception ("can't find Text object");
					}

					if("" == tileScript.GetTileText().text)
					{
						continue;
					}
					MapData.Tile tile = new MapData.Tile();
					tile.text = tileScript.GetTileText().text;
					tile.color = ColorToHex(tileScript.GetTileText().color);
					tile.type = tileScript.GetTileType().ToString();
					tile.x = x;
					tile.y = y;
					jd.tiles.Add(tile);
				}
			}
			StringBuilder sb = new StringBuilder();
			JsonWriter writer = new JsonWriter(sb);
			writer.PrettyPrint = true;
			writer.IndentValue = 2;
			JsonMapper.ToJson(jd, writer);
			Debug.Log (sb.ToString ());
			File.WriteAllText(path, sb.ToString());
		}

		public void Load(string path)
		{
			string json = File.ReadAllText(path);
			JsonData root = JsonMapper.ToObject (json);
			mapName = (string)root ["name"];
			JsonData size = root ["size"];
			width = (int)size ["width"];
			height = (int)size ["height"];

			Init (width, height);
			Transform contents = transform.FindChild ("Contents");
			if (null == contents) {
				throw new System.Exception ("can't find contents object");
			}
			JsonData tiles = root ["tiles"];
			for (int i=0; i<tiles.Count; i++) {
				JsonData jtile = tiles[i];
				int x = (int)jtile["x"];
				int y = (int)jtile["y"];

				Transform child = contents.GetChild(x + y * height);
				Tile tile = child.GetComponent<Tile>();
				tile.SetTileText((string)jtile["text"]);
				tile.SetTileColor(HexToColor((string)jtile["color"]));
			}
		}
		string ColorToHex(Color32 color)
		{
			string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
			return hex;
		}
		Color HexToColor(string hex)
		{
			byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
			byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
			byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
			return new Color32(r,g,b, 255);
		}

		void Init(int width, int height) {
			data.size.width = width;
			data.size.height = height;
			data.name = mapName;
			data.tiles.Clear ();

			Transform contents = transform.FindChild ("Contents");
			if (null == contents) {
				throw new System.Exception ("can't find contents object");
			}
			while(0<contents.childCount) {
				Transform child = contents.GetChild(0);
				child.SetParent(null);
				Destroy(child.gameObject);
			}
			RectTransform rt = contents.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (width * TILE_SIZE, height * TILE_SIZE);
			for (int y=0; y<height; y++) {
				for (int x=0; x<width; x++) {
					{
						Tile tile = Instantiate<Tile>(tilePref);
						tile.transform.SetParent (contents.transform, false);
						tile.transform.localPosition = new Vector3(x * TILE_SIZE, -y * TILE_SIZE, 0);
					}
					{
						MapData.Tile tile = new MapData.Tile();
						tile.text = "";
						tile.x = x;
						tile.y = y;
						data.tiles.Add(tile);
					}
				}
			}
		}
	}
}