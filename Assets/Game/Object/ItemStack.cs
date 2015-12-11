using UnityEngine;
using System.Collections;

public class ItemStack : Object {
	public ItemData item;
	public int count;
	private ObjectView view;
	public ItemStack() {
		category = Object.Category.Item;
	}

	public override void OnCreate()
	{
		view = MapView.Instance.AddItemStack (this);
	}
	public override void OnDestroy ()
	{
		if (null != view) {
			view.transform.SetParent(null);
			GameObject.Destroy (view.gameObject);
		}
		view = null;
	}
}