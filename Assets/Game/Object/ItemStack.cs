using UnityEngine;
using System.Collections;

public class ItemStack : Object {
	public ItemData item;
	public int count;
	private ObjectView view;
	public ItemStack() {
		size = 0.0f;
		category = Object.Category.Item;
	}

	public override void OnCreate()
	{
		view = ObjectView.Create<ObjectView>(this, "$", Color.yellow);
	}

	public override void SetPosition(Position position) {
		view.SetPosition (position);
		base.SetPosition (position);
	}

	public override void OnDestroy()
	{
		view.OnDestroy ();
	}
}