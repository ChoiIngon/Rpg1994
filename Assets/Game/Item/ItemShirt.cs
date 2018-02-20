using UnityEngine;
using System.Collections;

public class ShirtItemInfo : ItemInfo {
	public int defense;
	public int speed;

	public ShirtItemInfo() {
		category = ItemInfo.Category.Shirt;
	}
	/*
	public override ItemData CreateInstance() {
		ShirtItemData data = new ShirtItemData ();
		data.info = this;
		return data;
	}
	*/
}

public class ShirtItemData : EquipmentItemData {
	/*
	public override Character.Status GetStatus()
	{
		ShirtItemInfo shirt = (ShirtItemInfo)this.info;
		Character.Status status = new Character.Status ();
		status.defense = shirt.defense;
		status.speed = shirt.speed;
		return status;
	}
	*/
};