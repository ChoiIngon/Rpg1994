using UnityEngine;
using System.Collections;

public class PlayerView : CharacterView {
	public void SetObject(Object o) {
		base.SetObject (o, "@");
	}
	public void ShowArmorInfo(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		ScrollView.Instance.Add ("equip on ");
		ArmorItemInfo armorInfo = (ArmorItemInfo)info;
		
		switch (armorInfo.type) {
		case ArmorItemInfo.ItemType.Body :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Body);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Feet);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Hand);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Head);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			ScrollView.Instance.Add ("left hand", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.LeftRing);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on left hand");
			});
			ScrollView.Instance.Add (" or ");
			ScrollView.Instance.Add ("right hand\n", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.RightRing);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on right hand\n");
			});
			break;
		}
	}
	public void ShowSlotInfo(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		ScrollView.Instance.Add("weight:" + info.weight + ", cost:" + info.cost + "\n");
		ScrollView.Instance.Add(info.description + "\n");
		
		ScrollView.Instance.Add("You can ");

		switch (info.category) {
		case ItemInfo.Category.Weapon:
			ScrollView.Instance.Add ("equip", () => { 
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Weapon);	
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + "\n");
			});
			break;
		case ItemInfo.Category.Armor:
			ShowArmorInfo(slot);
			break;
		case ItemInfo.Category.Potion:
			ScrollView.Instance.Add ("drink", () => { 
				Game.Instance.player.UseItem (slot.index);	
				ShowInventory();
				ScrollView.Instance.Add ("You drink " + info.name + "\n");
			});
			break;
		}
			
		ScrollView.Instance.Add (" or ");
		ScrollView.Instance.Add ("drop\n\n", () => {
			ItemData item = Game.Instance.player.inventory.Pull(slot.index);
			CreateItemStackView(item);
			ShowInventory();
			ScrollView.Instance.Add ("You drop " + info.name + "\n");
		});
	}

	public void ShowInventory() {
		ScrollView.Instance.AddTitle ("Item Inventory");
		Player player = (Player)targetObject;
		Inventory inventory = player.inventory;
		for (int i=0; i<inventory.slots.Length; i++) {
			Inventory.Slot slot = inventory.slots[i];
			if(null == slot)
			{
				continue;
			}
			string type = ScrollView.MakeFixedLengthText(slot.item.info.category.ToString(), 8);
			string name = ScrollView.MakeFixedLengthText(slot.item.info.name, 20);
			ScrollView.Instance.Add (type + name + " " + slot.count + "\n", () => {
				ShowInventory(slot.index);
			});
		}
	}
	public void ShowInventory(int slotIndex) {
		ScrollView.Instance.AddTitle ("Item Inventory");
		Player player = (Player)targetObject;
		Inventory inventory = player.inventory;
		for (int i=0; i<inventory.slots.Length; i++) {
			Inventory.Slot slot = inventory.slots[i];
			if(null == slot)
			{
				continue;
			}
			string type = ScrollView.MakeFixedLengthText(slot.item.info.category.ToString(), 8);
			string name = ScrollView.MakeFixedLengthText(slot.item.info.name, 20);

			if(slotIndex == slot.index)
			{
				ScrollView.Instance.Add("<b><color=red>" + type + name + " " + slot.count + "</color></b>\n");
				ShowSlotInfo (slot);
			}
			else {
				ScrollView.Instance.Add (type + name + " " + slot.count + "\n", () => {
					ShowInventory(slot.index);
				});
			}
		}
	}
	public override void ShowItemInfo(ItemData item) {
		ItemInfo info = item.info;
		ScrollView.Instance.AddTitle (item.info.name);
		ScrollView.Instance.Add (ScrollView.MakeFixedLengthText("weight:" + info.weight, 10) + 
		                         ScrollView.MakeFixedLengthText(", cost:" + info.cost, 10) + "\n");
		ScrollView.Instance.Add (info.description + "\n");
		ScrollView.Instance.Add ("\n");
		ScrollView.Instance.Add ("You can ");
		/*
		switch (info.category) {
		case ItemInfo.Category.Weapon:
			ScrollView.Instance.Add ("unequip", () => { 
				Game.Instance.player..EquipWeaponItem (slot.index);	
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
		} 
		*/
		ScrollView.Instance.Add("\n");
	}
}
