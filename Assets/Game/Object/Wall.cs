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
		view = ObjectView.Create<WallView> (this, "#", Color.white);
	}

	public override void OnDestroy() {
		view.OnDestroy ();
	}

	public override void SetPosition(Object.Position position) {
		view.SetPosition (position);
		base.SetPosition (position);
	}
	public override void OnTrigger(Object obj) {
		if (Type.HiddenDoor == type) {
			int x = position.x;
			int y = position.y;
			this.Destroy();
			{
				Tile t = Map.Instance.GetTile (x-1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = Map.Instance.GetTile (x+1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = Map.Instance.GetTile (x, y-1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			{
				Tile t = Map.Instance.GetTile (x, y+1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
		}
	}
}
