using UnityEngine;
using System.Collections;

public class ItemStack : Object {
	public ItemData item;
	public int count;
	private ObjectView view;
	public ItemStack() {
		size = 0.0f;
		category = Object.Category.Item;
		onCreate += OnCreate;
	}

	public void OnCreate()
	{
		MapView.Instance.AddItemStack (this);
	}
}