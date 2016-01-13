using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public abstract class Object {
	public enum Category
	{
		Invalid,
		Monster,
		Player,
		NPC,
		Item,
		Wall
	}
	public Position position;
	public Category category;
	public float size;
	public int sight;
	public Object() {
		category = Category.Invalid;
		position = new Position(0, 0);
		sight = 1;
		size = 0.0f;
		OnCreate ();
	}

	public virtual void SetPosition(Position position) {
		if (Map.Instance.width <= position.x || 0 > position.x || Map.Instance.height <= position.y || 0 > position.y) {
			return;
		}

		if (this.position == position) {
			return;
		}
		Tile tile = Map.Instance.GetTile (position.x, position.y);
		tile.AddObject(this);
		this.position.x = position.x;
		this.position.y = position.y;
	}

	public virtual void Destroy() {
		Tile tile = Map.Instance.GetTile (position.x, position.y);
		tile.RemoveObject(this);
		OnDestroy ();
	}

	public virtual void OnCreate() {}
	public virtual void OnDestroy() {}
	public virtual void OnTrigger(Object obj) {}
	public virtual void Update() {}
}
