using UnityEngine;
using System.Collections;

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
	
	public Tile(int x, int y) {
		position.x = x;
		position.y = y;
		type = Type.Unused;
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
};