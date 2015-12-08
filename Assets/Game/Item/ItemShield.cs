using UnityEngine;
using System.Collections;

public class ShieldItemInfo : ItemInfo {
	public int defense;
	public int speed;

	public ShieldItemInfo() {
		category = ItemInfo.Category.Shield;
	}
	public override ItemData CreateInstance() {
		ShieldItemData data = new ShieldItemData ();
		data.info = this;
		return data;
	}
}

public class ShieldItemData : EquipmentItemData {
	public override Character.Status GetStatus()
	{
		ShieldItemInfo shild = (ShieldItemInfo)this.info;
		Character.Status status = new Character.Status ();
		status.defense = shild.defense;
		status.speed = shild.speed;
		return status;
	}
};
