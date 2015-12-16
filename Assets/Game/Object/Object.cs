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
	public Object() {
		category = Category.Invalid;
		position = new Position(0, 0);
		visible = true;
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
		//Debug.Log ("start:{x:" + s.x + ", y:" + s.y + "}, end:{x:" + e.x + ", y:" + e.y + "}");
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
			//Debug.Log ("x:" + s.x + ", y:" + s.y + ", p:" + p);
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
				//Debug.Log ("x:" + x + ", y:" + y + ", p:" + p);
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
			//Debug.Log ("x:" + s.x + ", y:" + s.y + ", p:" + p);
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
				//Debug.Log ("x:" + x + ", y:" + y + ", p:" + p);
			}
		}
		return positions;
	}

	public void SetPosition(Position position) {
		if (GameManager.Instance.map.width <= position.x || 0 > position.x || GameManager.Instance.map.height <= position.y || 0 > position.y) {
			return;
		}
		
		Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
		tile.AddObject(this);
		this.position = position;
	}

	public virtual void Destroy() {
		Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
		tile.RemoveObject(this);
		if (null != onDestroy) {
			onDestroy();
		}
	}
	public delegate void Delegate_OnCreate();
	public Delegate_OnCreate onCreate;
	public delegate void Delegate_OnDestroy();
	public Delegate_OnDestroy onDestroy;
}
