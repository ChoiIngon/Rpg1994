using UnityEngine;
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
		Tree,
		EnterPoint,
		Max
	}
	
	public Type type;
	public string id;
	public Color color;
	public bool visit;
	public Dictionary<Object, Object> objects;
	public TileView view;
	public Tile() {
		category = Object.Category.Tile;
		visit = false;
		type = Type.Floor;
		objects = new Dictionary<Object, Object> ();
	}
	
	public static Type ToType(string type)
	{
		if ("Floor" == type) {
			return Tile.Type.Floor;
		} else if ("Wall" == type) {
			return Tile.Type.Wall;
		} else if ("Tree" == type) {
			return Tile.Type.Tree;
		} else if ("EnterPoint" == type) {
			return Tile.Type.Floor;
		}
		throw new System.Exception("invalid tile type(" + type + ")");
	}

	public void AddObject(Object obj) {
		objects.Add (obj, obj);
	}

	public void RemoveObject(Object obj) {
		objects.Remove (obj);
	}

	public T FindObject<T>() where T : Object {
		foreach (var v in objects) {
			if(v.Value is T)
			{
				return (T)v.Value;
			}
		}
		return null;
	}

	public virtual void SetPosition(Position position) {
		if (GameManager.Instance.map.width <= position.x || 0 > position.x || GameManager.Instance.map.height <= position.y || 0 > position.y) {
			return;
		}
		
		if (this.position == position) {
			return;
		}
		this.position.x = position.x;
		this.position.y = position.y;
		view.SetPosition (position);
	}

	public override void OnCreate ()
	{
		view = ObjectView.Create<TileView> (this, ".", Color.white);
	}
	public override void OnDestroy() {
		view.OnDestroy ();
	}

	public override void Destroy() {
		OnDestroy ();
	}
};