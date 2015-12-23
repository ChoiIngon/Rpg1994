using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.EventSystems;

namespace MapEditor {
	public class Map : Util.UI.Singleton<Map> {
		private const int TILE_SIZE = 20;
		public int MAP_WIDTH;
		public int MAP_HEIGHT;
		public string mapName;
		public string mapDescription;
		public MapEditor.Tile tilePref;
        public Dungeon dungeon;
		// Use this for initialization
		void Start () {
			EnterPointDialog.Instance.gameObject.SetActive (false);
			TileImplFactory.Instance.Register ("Wall", Wall.Create);
			TileImplFactory.Instance.Register ("Tree", Tree.Create);
			TileImplFactory.Instance.Register ("Eraser", Eraser.Create);
			TileImplFactory.Instance.Register ("EnterPoint", EnterPoint.Create);
			TileImplFactory.Instance.Register ("ExitPoint", ExitPoint.Create);

			mapName = "";
			mapDescription = "";
			Init (MAP_WIDTH, MAP_HEIGHT);
			Load ("/Users/kukuta/workspace/Rpg1994/RegionInfo.json");
		}

		public void Save(string path) {
			Transform contents = transform.FindChild ("Contents");
			if (null == contents) {
				throw new System.Exception ("can't find contents object");
			}

			JSONNode root = new JSONClass();
			JSONArray tileNodes = new JSONArray ();
			root ["name"] = mapName;
			root ["description"] = mapDescription;
			root ["size"] ["width"].AsInt = MAP_WIDTH;
			root ["size"] ["height"].AsInt = MAP_HEIGHT;
			root ["tile"] = tileNodes;
			for(int y=0; y<MAP_HEIGHT; y++) {
				for(int x=0; x<MAP_WIDTH; x++) {
					Transform child = contents.GetChild(x + y * MAP_WIDTH);
					Tile tile = child.GetComponent<Tile>();
					if (null == tile) {
						throw new System.Exception ("can't find Text object");
					}

					if("" == tile.type)
					{
						continue;
					}
					tileNodes.Add(tile.ToJSON());
				}
			}
			File.WriteAllText (path, root.ToString ());
		}

		public void Load(string path)
		{
			if (false == System.IO.File.Exists (path)) {
				throw new System.Exception("can't find " + path + ", so create new region info");
			}
				
			string json = File.ReadAllText (path);
			JSONNode root = JSON.Parse (json);
			if (null == root ["name"]) {
				mapName = "";
			} else {
				mapName = root["name"];
			}
			if (null == root ["description"]) {
				mapDescription = "";
			} else {
				mapDescription = root["description"];
			}
			MAP_WIDTH = root ["size"] ["width"].AsInt;
			MAP_HEIGHT = root ["size"] ["height"].AsInt;
			Init (MAP_WIDTH, MAP_HEIGHT);
			JSONNode tileNodes = root ["tile"];
			for (int i=0; i<tileNodes.Count; i++) {
				JSONNode tileNode = tileNodes[i];
				int x = tileNode["x"].AsInt;
				int y = tileNode["y"].AsInt;

				Tile tile = GetTile (x, y);
				tile.FromJSON(tileNode);
			}
			Debug.Log ("read " + path + " map file.");
		}

		void Init(int width, int height) {
			Transform contents = transform.FindChild ("Contents");
			if (null == contents) {
				throw new System.Exception ("can't find contents object");
			}
			while(0<contents.childCount) {
				Transform child = contents.GetChild(0);
				child.SetParent(null);
				GameObject.DestroyImmediate(child.gameObject);
			}
			RectTransform rt = contents.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (width * TILE_SIZE, height * TILE_SIZE);
			for (int y=0; y<height; y++) {
				for (int x=0; x<width; x++) {
					MapEditor.Tile tile = Instantiate<MapEditor.Tile>(tilePref);
					tile.Init ();
					tile.position = new Object.Position(x, y);
					tile.transform.SetParent (contents.transform, false);
					tile.transform.localPosition = new Vector3(x * TILE_SIZE, -y * TILE_SIZE, 0);
				}
			}
			Debug.Log ("Initializing Map is completed");
		}

		void Update() {
			RectTransform rectTransform = GetComponent<RectTransform> ();
			int width = (int)rectTransform.rect.width;
			int height = (int)rectTransform.rect.height;
			Transform contents = transform.FindChild ("Contents");
			int x = (int)contents.localPosition.x;
			int y = (int)contents.localPosition.y;

			for (int i=0; i<contents.childCount; i++) {
				Transform child = contents.GetChild(i);
				if(child.localPosition.x + x < 0 - TILE_SIZE || child.localPosition.x + x > width + TILE_SIZE ||
				   child.localPosition.y + y > 0 + TILE_SIZE || child.localPosition.y + y < -1 * (height + TILE_SIZE))
				{
					child.gameObject.SetActive(false); 
				}
				else
				{
					child.gameObject.SetActive(true);
				}
			}
		}

		public Tile GetTile(int x, int y)
		{
			if (0 > x || MAP_WIDTH <= x || 0 > y || MAP_HEIGHT <= y) {
				return null;
			}
			Transform contents = transform.FindChild ("Contents");
			return contents.GetChild (x + y * MAP_WIDTH).GetComponent<Tile>();
		}
	}
}