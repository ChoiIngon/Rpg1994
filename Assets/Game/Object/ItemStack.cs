﻿using UnityEngine;
using System.Collections;

public class ItemStack : Object {
	public ItemData item;
	public int count;
	public ItemStack() {
		category = Object.Category.Item;
	}
}