using UnityEngine;
using System.Collections;

public class ItemView {
	private Inventory.Slot slot;
	private ItemData data;
	public ItemView(Inventory.Slot slot)
	{
		this.slot = slot;
		this.data = this.slot.item;
	}
	public ItemView(ItemData data) {
		this.slot = null;
		this.data = data;
	}

	public void ShowArmorInfo(ItemInfo info) {
		/*
		ScrollView.Instance.Add ("equip on ");
		ArmorItemInfo armorInfo = (ArmorItemInfo)info;
		
		switch (armorInfo.type) {
		case ArmorItemInfo.ItemType.Body :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Body);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Feet);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Hand);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Head);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			ScrollView.Instance.Add ("left hand", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.LeftRing);
				ScrollView.Instance.Add ("You equip " + info.name + " on left hand");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			ScrollView.Instance.Add (" or ");
			ScrollView.Instance.Add ("right hand\n", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.RightRing);
				ScrollView.Instance.Add ("You equip " + info.name + " on right hand\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		}
		*/
	}
	public void ShowInfo() {
		/*
		ItemInfo info = this.data.info;
		ScrollView.Instance.Add ("========== <b>" + info.name + "</b> ==========\n");
		ScrollView.Instance.Add ("weight:" + info.weight + ", cost:" + info.cost + "\n");
		ScrollView.Instance.Add (info.description + "\n");

		ScrollView.Instance.Add ("You can ");
		if (null != slot) {
			switch (info.category) {
			case ItemInfo.Category.Weapon:
				ScrollView.Instance.Add ("equip", () => { 
					Game.Instance.player.EquipWeaponItem (slot.index);	
					ScrollView.Instance.Add ("You equip " + info.name + "\n");
					InventoryView view = new InventoryView (Game.Instance.player.inventory);
					view.ShowInfo ();
				});
				break;
			case ItemInfo.Category.Armor:
				ShowArmorInfo(info);
				break;
			case ItemInfo.Category.Potion:
				ScrollView.Instance.Add ("drink", () => { 
					Game.Instance.player.UseItem (slot.index);	
					ScrollView.Instance.Add ("You drink " + info.name + "\n");
					InventoryView view = new InventoryView (Game.Instance.player.inventory);
					view.ShowInfo ();
				});
				break;
			}

			ScrollView.Instance.Add (" or ");
			ScrollView.Instance.Add ("drop\n", () => {
			});
		} else {
		}
		*/
	}
}
