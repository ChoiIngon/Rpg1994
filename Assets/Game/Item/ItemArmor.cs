using UnityEngine;
using System.Collections;

public class ArmorItemInfo : ItemInfo {
	public enum ItemType
	{
		Ring,
		Neck,
		Head,
		Hand,
		Feet,
		Legs,
		Body,
		Max
	}
	public int defense;
	public int speed;
	public int durability;
	public ItemType type;

	public static ItemType ToType(string type) {
		if ("ring" == type) {
			return ItemType.Ring;
		} else if ("neck" == type) {
			return ItemType.Neck;
		} else if ("head" == type) {
			return ItemType.Head;
		} else if ("hand" == type) {
			return ItemType.Hand;
		} else if ("feet" == type) {
			return ItemType.Feet;
		} else if ("legs" == type) {
			return ItemType.Legs;
		} else if ("body" == type) {
			return ItemType.Body;
		}
		throw new System.Exception("invalid armor item type(" + type + ")");
	}
	public override ItemData CreateInstance() {
		ArmorItemData data = new ArmorItemData ();
		data.info = this;
		return data;
	}
}

public class ArmorItemData : EquipmentItemData {
	public int durability;
};
