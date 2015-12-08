using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionItemInfo : ItemInfo {
	public List<BuffInfo> buff = new List<BuffInfo> ();
	public PotionItemInfo() {
		category = ItemInfo.Category.Potion;
	}
	public override ItemData CreateInstance() {
		PotionItemData data = new PotionItemData ();
		data.info = this;
		return data;
	}
}

public class PotionItemData : ItemData {
}