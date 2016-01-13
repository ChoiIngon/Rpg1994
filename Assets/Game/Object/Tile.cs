using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : Object {
	public bool visit;
	public bool visible;
	public Dictionary<Object, Object> objects;
	public TileView view;
	public Tile() {
		visit = false;
		visible = false;
		objects = new Dictionary<Object, Object> ();
		OnCreate ();
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
		if (Map.Instance.width <= position.x || 0 > position.x || Map.Instance.height <= position.y || 0 > position.y) {
			return;
		}

		this.position.x = position.x;
		this.position.y = position.y;
		view.SetPosition (position);
	}

	public void OnCreate ()
	{
		view = ObjectView.Create<TileView> (this, ".", Color.white);
	}

	public void OnDestroy() {
		view.OnDestroy ();
	}

	public float GetObjectSize() {
		float size = 0.0f;
		foreach (var v in objects) {
			size += v.Value.size;
		}
		return size;
	}
};