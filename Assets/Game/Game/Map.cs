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
			tile.position.x = x;
			tile.position.y = y;

			tile.id = (string)jtile["text"];
			tile.type = Tile.ToType((string)jtile["type"]);
			tile.color = HexToColor((string)jtile["color"]);
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
