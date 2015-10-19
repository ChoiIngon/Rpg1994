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
			tile.id = tileNode["text"];
			tile.type = Tile.ToType(tileNode["type"]);
			tile.color = HexToColor(tileNode["color"]);
		}
		Game.Instance.player.FieldOfView ();
	}

	public Tile GetTile(int x, int y) {
		return tiles [x + y * width];
	}

	/*
	 * public MonsterData FindTarget(int x, int y) {
		foreach(var v in MonsterManager.Instance.monsters)
		{
			MonsterData monster = v.Value;
			if(monster.position.x == x && monster.position.y == y) {
				return monster;
			}
		}
		return null;
	}
	*/
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
}
