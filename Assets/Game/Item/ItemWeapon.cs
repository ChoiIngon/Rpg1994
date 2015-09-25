using UnityEngine;
using System.Collections;

public class WeaponItemInfo : ItemInfo {
	public enum ItemType {
		Sword,
		Sphere,
		Dagger,
		Max
	}
	public RangeInt attack = new RangeInt();
	public int speed;
	public int durability;
	public ItemType type;

	public static ItemType ToType(string type) {
		if ("sword" == type) {
			return ItemType.Sword;
		} else if ("sphere" == type) {
			return ItemType.Sphere;
		} else if ("dagger" == type) {
			return ItemType.Dagger;
		}
		throw new System.Exception("invalid weapon item type(" + type + ")");
	}
	public override ItemData CreateInstance()
	{
		WeaponItemData data = new WeaponItemData ();
		data.info = this;
		data.durability = durability;
		return data;
	}
};

public class WeaponItemData : EquipmentItemData {
	public int durability;
}
