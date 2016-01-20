using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public abstract class MapImpl {
	public int width;
	public int height;
	public Tile[] tiles;
	public Position start;

	public abstract void Generate();
	public abstract void Update();
}

public class Map : Util.Singleton<Map> {
	public MapView view;
	public MapImpl impl;

	public string id;
	public string name;
	public string description;

	public int width { get { return impl.width; } }
	public int height { get { return impl.height; } }
	public Tile[] tiles { get { return impl.tiles; } }
	public Position start { get { return impl.start; } }
	
	public void Init() {
		view = GameObject.Find ("MapView").GetComponent<MapView>();
		view.tiles = GameObject.Find ("MapView/Tiles").transform;
	}

	public void Load(string path)
	{
		id = path;

        TextAsset json = Resources.Load(path) as TextAsset;
		JSONNode root = JSON.Parse (json.text);

		name = root ["name"];
		description = root ["description"];

		if ("dungeon" == (string)root ["type"]) {
			impl = new Dungeon (root);
		}

		view.Init ();
		impl.Generate ();

		Player.Instance.OnCreate ();
		Player.Instance.SetPosition (Map.Instance.start);
		Update ();
	}

	public Tile GetTile(int x, int y) {
		if (width <= x || 0 > x || height <= y || 0 > y) {
			return null;
		}
		return tiles [x + y * width];
	}

	public void Update()
	{
		List<Object> objects = new List<Object> ();
		foreach(Tile tile in tiles) {
			tile.visible = false;
			foreach(var v in tile.objects) {
				objects.Add(v.Value);
			}
		}

		foreach (Object obj in objects) {
			obj.Update();
		}
		impl.Update ();

		Map.Instance.FieldOfView (Player.Instance.position, Player.Instance.sight);
		view.Center ();
	}

	public void FieldOfView(Position src, int range)
    {
        Util.BresenhamCircle2D circle = new Util.BresenhamCircle2D(src, range);
        foreach (Position circumference in circle)
        {
            Util.BresenhamLine2D line = new Util.BresenhamLine2D(src, circumference);
            foreach (Position position in line)
            {
                Tile tile = GetTile(position.x, position.y);
                tile.visit = true;
                tile.visible = true;
                if (1.0f < tile.GetObjectSize())                          
                {
                    break;
                }
            }
        }
    }
}
