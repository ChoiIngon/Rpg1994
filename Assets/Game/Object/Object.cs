using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public abstract class Object {
	public class Position {
		public int x;
		public int y;
		public Position(int x, int y) {
			this.x = x;
			this.y = y;
		}
		public override string ToString() {
			return "Position:{x:" + x + ", y:" + y + "}";
		}
		public static bool operator == (Position rhs, Position lhs) {
			if(object.ReferenceEquals(null, rhs)) {
				return object.ReferenceEquals(null, lhs);
			}
			if (object.ReferenceEquals (null, lhs)) {
				return object.ReferenceEquals (null, rhs);
			}
			return rhs.x == lhs.x && rhs.y == lhs.y;
		}
		public static bool operator != (Position rhs, Position lhs) {
			if (object.ReferenceEquals (null, rhs)) {
				return !object.ReferenceEquals (null, lhs);
			}
			if (object.ReferenceEquals (null, lhs)) {
				return !object.ReferenceEquals(null, rhs);
			}
			return rhs.x != lhs.x || rhs.y != lhs.y;
		}
		public override bool Equals(object p) {
			return this == (Position)p;
		}
		public override int GetHashCode() {
			return ToString ().GetHashCode ();
		}
		public static implicit operator Vector2 (Position p) {
			return new Vector2(p.x, p.y);
		}
	}

	public enum Category
	{
		Invalid,
		Tile,
		Monster,
		Player,
		NPC,
		Item,
		Wall
	}
	public Position position;
	public bool visible;
	public Category category;
	public float size;
	public int sight;
	public Object() {
		category = Category.Invalid;
		position = new Position(0, 0);
		visible = true;
		sight = 1;
		size = 0.0f;
		OnCreate ();
	}

	public List<Position> Raycast(Object dest) {
		return Object.Raycast (position, dest.position);
	}

	public List<Position> Raycast(Position dest) {
		return Object.Raycast (position, dest);
	}

	public static List<Position> Raycast(Object s, Object e) {
		return Object.Raycast (s.position, e.position);
	}

	public static List<Position> Raycast(Position s, Position e)
	{
		List<Position> positions = new List<Position> ();

		int dx = Math.Abs(e.x - s.x);
		int dy = Math.Abs(e.y - s.y);
		if (0 == dx && 0 == dy) {
			return positions;
		}
		if(dy <= dx)
		{
			int p = 2 * (dy - dx);
			int y = s.y;

			int inc_x = 1;
			if(e.x < s.x) 
			{
				inc_x = -1;
			}
			int inc_y = 1;
			if(e.y < s.y) {
				inc_y = -1;
			}
			for(int x=s.x; (e.x > s.x ? x <= e.x : x>=e.x) ; x += inc_x) {
				if(0 >= p) {
					p += 2 * dy;
				}
				else {
					p += 2 * (dy - dx);
					y += inc_y;
				}
				Position position = new Position(x, y);
				positions.Add(position);
			}
		}
		else {
			int p = 2 * (dx - dy);
			int x = s.x;
			
			int inc_x = 1;
			if(e.x < s.x) {
				inc_x = -1;
			}
			int inc_y = 1;
			if(e.y < s.y) {
				inc_y = -1;
			}
			for(int y=s.y; (e.y > s.y ? y <= e.y : y>=e.y) ; y += inc_y) {
				if(0 >= p) {
					p += 2 * dx;
				}
				else {
					p += 2 * (dx - dy);
					x += inc_x;
				}
				Position position = new Position(x, y);
				positions.Add(position);
			}
		}
		return positions;
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

	private void CheckVisible(Object.Position dest)
	{
		List<Object.Position> positions = Raycast(dest);
		foreach(Object.Position position in positions) {
			if(sight < Vector2.Distance(this.position, position)) {
				return;
			}
			
			Tile tile = Map.Instance.GetTile (position.x, position.y);
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
	public void FieldOfView() {
		Object.Position src = position;
		{
			int y = Math.Max(0, src.y - sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Map.Instance.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int y = Math.Min(Map.Instance.height-1, src.y + sight);
			for(int x=Math.Max (0, src.x - sight); x < Math.Min (src.x + sight, Map.Instance.width); x++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Max(0, src.x - sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Map.Instance.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
		{
			int x = Math.Min(Map.Instance.width-1, src.x + sight);
			for(int y=Math.Max (0, src.y - sight); y < Math.Min (src.y + sight, Map.Instance.height); y++)
			{
				CheckVisible(new Object.Position(x, y));
			}
		}
	}
	public virtual void OnCreate() {}
	public virtual void OnDestroy() {}
	public virtual void OnTrigger(Object obj) {}
	public virtual void Update() {}
}
