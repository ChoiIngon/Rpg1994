using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemInfo {
	public enum Category {
		Weapon,
		Shield,
		//Helmet,
		Shirt,
		Ring,
		//Necklace,
		//Glove,
		//Shoes,
		//Pants,
		Potion,
		//Key,
		Max
	}

	public string id;
	public string name;
	public string description;
	public int weight;
	public int cost;
	public Category category;
	//public abstract ItemData CreateInstance();
}

public abstract class ItemData {
	public ItemInfo info;
	/*
	public virtual Character.Status Use (Character character) {
		throw new System.Exception ("the item can not be used");
	}
	*/
}

public abstract class EquipmentItemData : ItemData {
	public int seq;
	/*
	public Character.EquipPart part;
	public abstract Character.Status GetStatus();
	*/
}