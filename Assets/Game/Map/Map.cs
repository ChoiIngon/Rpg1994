﻿using UnityEngine;
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
				v.Value.visible = false;
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

	public void LineOfView(Position src, Position dest, int range)
	{
		List<Position> positions = Position.Raycast(src, dest);
		foreach(Position position in positions) {
			if(range < Vector2.Distance(src, position)) {
				return;
			}
			
			Tile tile = GetTile (position.x, position.y);
			tile.visit = true;
			tile.visible = true;
			foreach(var v in tile.objects) {
				v.Value.visible = true;
				if(1.0f < v.Value.size)
				{
					return;
				}
			}
		}
	}

	public void FieldOfView(Position src, int range) {
		{
			int y = Math.Max(0, src.y - range);
			for(int x=Math.Max (0, src.x - range); x < Math.Min (src.x + range, width); x++)
			{
				LineOfView(src, new Position(x, y), range);
			}
		}
		{
			int y = Math.Min(height-1, src.y + range);
			for(int x=Math.Max (0, src.x - range); x < Math.Min (src.x + range, width); x++)
			{
				LineOfView(src, new Position(x, y), range);
			}
		}
		{
			int x = Math.Max(0, src.x - range);
			for(int y=Math.Max (0, src.y - range); y < Math.Min (src.y + range, height); y++)
			{
				LineOfView(src, new Position(x, y), range);
			}
		}
		{
			int x = Math.Min(Map.Instance.width-1, src.x + range);
			for(int y=Math.Max (0, src.y - range); y < Math.Min (src.y + range, height); y++)
			{
				LineOfView(src, new Position(x, y), range);
			}
		}
	}
}
