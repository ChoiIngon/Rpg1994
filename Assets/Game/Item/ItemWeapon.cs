using UnityEngine;
using System.Collections;

public class WeaponItemInfo : ItemInfo {
	public Util.RangeInt attack = new Util.RangeInt();
	public int speed;

	public WeaponItemInfo() {
		category = ItemInfo.Category.Weapon;
	}
	public override ItemData CreateInstance()
	{
		WeaponItemData data = new WeaponItemData ();
		data.info = this;
		return data;
	}
};

public class WeaponItemData : EquipmentItemData {
	public override Character.Status GetStatus()
	{
		WeaponItemInfo weapon = (WeaponItemInfo)this.info;
		Character.Status status = new Character.Status ();
		status.attack = weapon.attack;
		status.speed = weapon.speed;
		return status;
	}
}
