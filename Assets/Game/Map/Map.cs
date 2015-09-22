using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class Map {
	public int width;
	public int height;

	public Tile[] tiles;
	public ItemData[] items;
	
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
	}

	public void Load(string path)
	{
		TextAsset json = Resources.Load(path) as TextAsset;
		JsonData root = JsonMapper.ToObject (json.text);
		JsonData size = root ["size"];
		width = (int)size ["width"];
		height = (int)size ["height"];
		
		Init (width, height);

		JsonData jtiles = root ["tiles"];
		for (int i=0; i<jtiles.Count; i++) {
			JsonData jtile = jtiles[i];
			int x = (int)jtile["x"];
			int y = (int)jtile["y"];
			
			Tile tile = GetTile(x, y);
			tile.id = (string)jtile["text"];
			tile.type = Tile.ToType((string)jtile["type"]);
			tile.color = Color.white;
		}
		for(int i=0; i<15; i++) {
			MonsterData monster = MonsterManager.Instance.CreateInstance("monster_001");
			monster.position.x = Random.Range(0, width-1);
			monster.position.y = Random.Range(0, height-1);
		}
		Game.Instance.player.FieldOfView ();
	}

	public Tile GetTile(int x, int y) {
		return tiles [x + y * width];
	}

	public MonsterData FindTarget(int x, int y) {
		foreach(var v in MonsterManager.Instance.monsters)
		{
			MonsterData monster = v.Value;
			if(monster.position.x == x && monster.position.y == y) {
				return monster;
			}
		}
		return null;
	}

}
