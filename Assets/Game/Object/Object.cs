using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public abstract class Object {
	public Position position = new Position();
	public override string ToString() {
		return "'position':{'x':" + position.x + ", 'y':" + position.y + "}";
	}
}
