using UnityEngine;
using System.Collections;

public class Gateway : Object {
	public class Destination {
		public string mapID;
		public Object.Position position;
	}
	
	public ObjectView view;
	public Destination dest = new Destination();

	public Gateway(int x, int y) {
		size = 1.0f;
		OnCreate ();
	}

	public override void OnCreate() {
		view = ObjectView.Create<ObjectView> (this, ">", Color.white);
		view.position = position;
		view.transform.SetParent (GameManager.Instance.map.view.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void SetPosition(Object.Position position)
	{
		base.SetPosition (position);
		view.position = position;
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public void MoveMap() {
		GameManager.Instance.Init ();
		GameManager.Instance.player.SetPosition (dest.position);
	}
	public override void OnDestroy() {
	}

	public override void OnTrigger(Object obj) {
		PopupMessageView.Instance.AddSubmitListener (() => {
			this.MoveMap();
		});
		PopupMessageView.Instance.SetText ("Do you want to move to " + dest.mapID + "?");
		PopupMessageView.Instance.SetWidth (500);
		PopupMessageView.Instance.gameObject.SetActive (true);
	}
}
