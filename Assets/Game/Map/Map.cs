using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public abstract class MapImpl {
	public int width;
	public int height;

	public abstract void Generate();
}

public class Map {
	public MapView view;
	public MapImpl impl;
	public string id;
	public int width;
	public int height;
	public string name;
	public string description;
	public Tile[] tiles;
	public ItemData[] items;
    public List<MonsterSpawnSpot> monsterSpawnSpots;
	public Object.Position start;
	private void Init() {
		view = GameObject.Find ("MapView").GetComponent<MapView>();
		view.Init (this);

		items = new ItemData[width * height];
		monsterSpawnSpots = new List<MonsterSpawnSpot>();
		tiles = new Tile[width * height];
		for (int y=0; y<height; y++) {
			for(int x=0;x<width; x++) {
				Tile tile = new Tile();
				tile.SetPosition(new Object.Position(x, y));
				tiles[x + y * width] = tile;
			}
		}
	}

	public void Load(string path)
	{
        TextAsset json = Resources.Load(path) as TextAsset;
		JSONNode root = JSON.Parse (json.text);
		id = path;
		name = root ["name"];
		description = root ["description"];
		/*
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
			if(Tile.Type.Wall == tile.type)
			{
				Wall wall = new Wall();
				wall.SetPosition(tile.position);
			}
		}
		*/
		Dungeon dungeon = new Dungeon (root);
		width = dungeon.width;
		height = dungeon.height;
		Init ();
		dungeon.Generate();
		monsterSpawnSpots = dungeon.GenerateMonster ();
		start = dungeon.start;
	}

	public Tile GetTile(int x, int y) {
		if (width <= x || 0 > x || height <= y || 0 > y) {
			return null;
		}
		return tiles [x + y * width];
	}

    public void AddMonsterRegenSpot(MonsterSpawnSpot spot)
	{
        monsterSpawnSpots.Add(spot);
	}

	public void AddMonster(MonsterSpawnSpot spot)
	{
		monsterSpawnSpots.Add(spot);
	}
	public void Update()
	{
		foreach(Tile tile in tiles) {
			tile.visible = false;
			foreach(var v in tile.objects) {
				v.Value.visible =  false;
			}
		}

        foreach (MonsterSpawnSpot spot in monsterSpawnSpots)
        {
			spot.Update();
		}
	}
}
