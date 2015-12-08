using UnityEngine;
using System.Collections;

public class RingItemInfo : ItemInfo {
	public RingItemInfo() {
		category = ItemInfo.Category.Ring;
	}
	public override ItemData CreateInstance() {
		RingItemData data = new RingItemData ();
		data.info = this;
		return data;
	}
}

public class RingItemData : EquipmentItemData {
	public override Character.Status GetStatus()
	{
		Character.Status status = new Character.Status ();
		return status;
	}
}
