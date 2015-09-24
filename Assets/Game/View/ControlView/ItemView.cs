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
		LogView.Instance.Add ("equip on ");
		ArmorItemInfo armorInfo = (ArmorItemInfo)info;
		
		switch (armorInfo.type) {
		case ArmorItemInfo.ItemType.Body :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Body);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Feet);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Hand);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Head);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			LogView.Instance.Add ("left hand", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.LeftRing);
				LogView.Instance.Add ("You equip " + info.name + " on left hand");
				InventoryView view = new InventoryView (Game.Instance.player.inventory);
				view.ShowInfo ();
			});
			LogView.Instance.Add (" or ");
			LogView.Instance.Add ("right hand\n", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.RightRing);
				LogView.Instance.Add ("You equip " + info.name + " on right hand\n");
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
		LogView.Instance.Add ("========== <b>" + info.name + "</b> ==========\n");
		LogView.Instance.Add ("weight:" + info.weight + ", cost:" + info.cost + "\n");
		LogView.Instance.Add (info.description + "\n");

		LogView.Instance.Add ("You can ");
		if (null != slot) {
			switch (info.category) {
			case ItemInfo.Category.Weapon:
				LogView.Instance.Add ("equip", () => { 
					Game.Instance.player.EquipWeaponItem (slot.index);	
					LogView.Instance.Add ("You equip " + info.name + "\n");
					InventoryView view = new InventoryView (Game.Instance.player.inventory);
					view.ShowInfo ();
				});
				break;
			case ItemInfo.Category.Armor:
				ShowArmorInfo(info);
				break;
			case ItemInfo.Category.Potion:
				LogView.Instance.Add ("drink", () => { 
					Game.Instance.player.UseItem (slot.index);	
					LogView.Instance.Add ("You drink " + info.name + "\n");
					InventoryView view = new InventoryView (Game.Instance.player.inventory);
					view.ShowInfo ();
				});
				break;
			}

			LogView.Instance.Add (" or ");
			LogView.Instance.Add ("drop\n", () => {
			});
		} else {
		}
		*/
	}
}
