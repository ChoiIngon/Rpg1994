using UnityEngine;
using System.Collections;

public class Gateway : Object {
	public class Destination {
		public string name;
		public string id;
		public string map;
	}
	
	public ObjectView view;
	public Destination dest = new Destination();

	public Gateway() {
		size = 0.5f;
	}

	public override void OnCreate() {
		view = ObjectView.Create<WallView> (this, ">", Color.white);
	}

	public override void SetPosition(Position position)
	{
		base.SetPosition (position);
		view.SetPosition (position);
	}

	public override void OnDestroy() {
	}

	public override void OnTrigger(Object obj) {
		PopupMessageView.Instance.AddSubmitListener (() => {
			this.MoveMap();
		});
		PopupMessageView.Instance.SetText ("Do you want to move to " + dest.name + "?");
		PopupMessageView.Instance.SetWidth (500);
		PopupMessageView.Instance.gameObject.SetActive (true);
	}
	private void MoveMap() {
		Map.Instance.Load (dest.map);
	}

}
