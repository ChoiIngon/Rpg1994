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
	public WallView view;
	public Wall() {
		type = Type.Wall;
		size = 1.1f;
	}
	public override void OnCreate() {
		view = ObjectView.Create<WallView> (this, ".", Color.white);
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(view.position.x * MapView.TILE_SIZE, -view.position.y * MapView.TILE_SIZE, 0);
		view.Init (this);
	}
	public override void OnDestroy() {
		view.transform.SetParent (null);
		GameObject.Destroy (view.gameObject);
	}

	public override void OnTrigger(Object obj) {
		if (Type.HiddenDoor == type) {
			int x = position.x;
			int y = position.y;
			this.Destroy();
			{
				Tile t = GameManager.Instance.map.GetTile (x-1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = GameManager.Instance.map.GetTile (x+1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = GameManager.Instance.map.GetTile (x, y-1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = GameManager.Instance.map.GetTile (x, y+1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
		}
	}
}
