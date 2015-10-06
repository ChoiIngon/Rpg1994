﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : Object {
	public enum Type {
		Unused,
		Floor,
		Corridor,
		Wall,
		ClosedDoor,
		OpenDoor,
		UpStairs,
		DownStairs,
		Max
	}
	
	public Type type;
	public string id;
	public Color color;
	public Dictionary<Object, Object> objects;
	public Tile(int x, int y) {
		category = Object.Category.Tile;
		position.x = x;
		position.y = y;
		type = Type.Unused;
		objects = new Dictionary<Object, Object> ();
	}
	
	public static Type ToType(string type)
	{
		if ("Floor" == type) {
			return Tile.Type.Floor;
		} else if ("Wall" == type) {
			return Tile.Type.Wall;
		} 
		throw new System.Exception("invalid tile type(" + type + ")");
	}

	public void AddObject(Object obj) {
		objects.Add (obj, obj);
	}

	public void RemoveObject(Object obj) {
		objects.Remove (obj);
	}
};