using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Position {
	public int x = 0;
	public int y = 0;

	public Position() {
		this.x = 0;
		this.y = 0;
	}
	public Position(int x, int y) {
		this.x = x;
		this.y = y;
	}
	public override string ToString() {
		return "'position':{'x':" + x + ", 'y':" + y + "}";
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