﻿using UnityEngine;
using System.Collections;

public class PlayerView : CharacterView {
	public void ShowArmorInfo(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		LogView.Instance.Add ("equip on ");
		ArmorItemInfo armorInfo = (ArmorItemInfo)info;
		
		switch (armorInfo.type) {
		case ArmorItemInfo.ItemType.Body :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Body);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Feet);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Hand);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Head);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			LogView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			LogView.Instance.Add ("left hand", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.LeftRing);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on left hand");
			});
			LogView.Instance.Add (" or ");
			LogView.Instance.Add ("right hand\n", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.RightRing);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on right hand\n");
			});
			break;
		}
	}
	public void ShowSlotInfo(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		LogView.Instance.Add("weight:" + info.weight + ", cost:" + info.cost + "\n");
		LogView.Instance.Add(info.description + "\n");
		
		LogView.Instance.Add("You can ");

		switch (info.category) {
		case ItemInfo.Category.Weapon:
			LogView.Instance.Add ("equip", () => { 
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Weapon);	
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + "\n");
			});
			break;
		case ItemInfo.Category.Armor:
			ShowArmorInfo(slot);
			break;
		case ItemInfo.Category.Potion:
			LogView.Instance.Add ("drink", () => { 
				Game.Instance.player.UseItem (slot.index);	
				ShowInventory();
				LogView.Instance.Add ("You drink " + info.name + "\n");
			});
			break;
		}
			
		LogView.Write (" or ");
		LogView.Button ("drop\n\n", () => {
			ItemData item = Game.Instance.player.inventory.Pull(slot.index);
			OnDropItem(item);
			ShowInventory();
			LogView.Write ("You drop " + info.name + "\n");
		});
	}

	public void ShowInventory() {
		LogView.Instance.AddTitle ("Item Inventory");
		Player player = (Player)targetObject;
		Inventory inventory = player.inventory;
		for (int i=0; i<inventory.slots.Length; i++) {
			Inventory.Slot slot = inventory.slots[i];
			if(null == slot)
			{
				continue;
			}
			string type = LogView.MakeFixedLengthText(slot.item.info.category.ToString(), 8);
			string name = LogView.MakeFixedLengthText(slot.item.info.name, 20);
			LogView.Instance.Add (type + name + " " + slot.count + "\n", () => {
				ShowInventory(slot.index);
			});
		}
	}
	public void ShowInventory(int slotIndex) {
		LogView.Instance.AddTitle ("Item Inventory");
		Player player = (Player)targetObject;
		Inventory inventory = player.inventory;
		for (int i=0; i<inventory.slots.Length; i++) {
			Inventory.Slot slot = inventory.slots[i];
			if(null == slot)
			{
				continue;
			}
			string type = LogView.MakeFixedLengthText(slot.item.info.category.ToString(), 8);
			string name = LogView.MakeFixedLengthText(slot.item.info.name, 20);

			if(slotIndex == slot.index)
			{
				LogView.Instance.Add("<b><color=red>" + type + name + " " + slot.count + "</color></b>\n");
				ShowSlotInfo (slot);
			}
			else {
				LogView.Instance.Add (type + name + " " + slot.count + "\n", () => {
					ShowInventory(slot.index);
				});
			}
		}
	}
	public override void ShowItemInfo(ItemData item) {
		ItemInfo info = item.info;
		LogView.Instance.AddTitle (item.info.name);
		LogView.Instance.Add (LogView.MakeFixedLengthText("weight:" + info.weight, 10) + 
		                         LogView.MakeFixedLengthText(", cost:" + info.cost, 10) + "\n");
		LogView.Instance.Add (info.description + "\n");
		LogView.Instance.Add ("\n");
		LogView.Instance.Add ("You can ");
		/*
		switch (info.category) {
		case ItemInfo.Category.Weapon:
			LogView.Instance.Add ("unequip", () => { 
				Game.Instance.player..EquipWeaponItem (slot.index);	
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
		} 
		*/
		LogView.Instance.Add("\n");
	}
}
