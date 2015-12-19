using UnityEngine;
using System.Collections;

public class Gateway : Object {
	public class Destination {
		public string mapID;
		public Object.Position position;
	}
	
	public ObjectView view;
	public Destination dest = new Destination();

	public Gateway() {
		size = 1.0f;
		OnCreate ();
	}
	public override void OnCreate() {
		view = ObjectView.Create<ObjectView> (this, ">", Color.white);
		view.SetVisible (false);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void SetPosition(Object.Position position)
	{
		base.SetPosition (position);
		view.position = position;
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnDestroy() {
	}
}
