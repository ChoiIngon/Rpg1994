using UnityEngine;
using System.Collections;

public class KeyItemInfo : ItemInfo {
	public enum ItemType {
		GoldenKey,
		SilverKey,
		CopperKey
	}
	public ItemType type;
	public override ItemData CreateInstance() {
		KeyItemData data = new KeyItemData ();
		data.info = this;
		return data;
	}
	public static ItemType ToType(string type) {
		if ("gold" == type) {
			return ItemType.GoldenKey;
		} else if ("silver" == type) {
			return ItemType.SilverKey;
		} else if ("coper" == type) {
			return ItemType.CopperKey;
		} 
		throw new System.Exception("invalid key item type(" + type + ")");
	}
}

public class KeyItemData : ItemData {
}
