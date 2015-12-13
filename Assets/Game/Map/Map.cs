using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Map {
	public int width;
	public int height;
	public string name;
	public string description;
	public Tile[] tiles;
	public ItemData[] items;
	public List<MonsterRegenSpot> monsterRegenSpots;

	private void Init(int width, int height) {
		tiles = new Tile[width * height];
		for (int y=0; y<height; y++) {
			for(int x=0;x<width; x++) {
				Tile tile = new Tile(x, y);
				tile.id = "";
				tile.type = Tile.Type.Floor;
				tile.color = Color.white;
				tiles[x+y*width] = tile;
			}
		}
		items = new ItemData[width * height];
		monsterRegenSpots = new List<MonsterRegenSpot> ();
	}

	public void Load(string path)
	{
		TextAsset json = Resources.Load(path) as TextAsset;
		JSONNode root = JSON.Parse (json.text);
		name = root ["name"];
		description = root ["description"];
		width = root ["size"] ["width"].AsInt;
		height = root ["size"] ["height"].AsInt;
		Init (width, height);
		JSONNode tileNodes = root ["tile"];
		for (int i=0; i<tileNodes.Count; i++) {
			JSONNode tileNode = tileNodes[i];
			int x = tileNode["x"].AsInt;
			int y = tileNode["y"].AsInt;
			Tile tile = GetTile(x, y);
			if(null == tile) {
				throw new System.Exception("out of map position");
			}
			tile.id = tileNode["text"];
			tile.type = Tile.ToType(tileNode["type"]);
			tile.color = Util.Color.HexToColor(tileNode["color"]);
		}
	}

	public Tile GetTile(int x, int y) {
		if (width <= x || 0 > x || height <= y || 0 > y) {
			return null;
		}
		return tiles [x + y * width];
	}
	
	public void AddMonsterRegenSpot(MonsterRegenSpot spot)
	{
		monsterRegenSpots.Add (spot);
	}

	public void Update()
	{
		foreach (MonsterRegenSpot spot in monsterRegenSpots) {
			spot.Update();
		}
	}
}
