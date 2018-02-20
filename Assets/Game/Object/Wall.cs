using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : Object {
	public enum Type
	{
		HiddenDoor,
		Wall
	}
	public Type type;
	public Wall() {
		type = Type.Wall;
	}
}
