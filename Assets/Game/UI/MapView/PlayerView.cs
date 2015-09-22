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
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Body);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Feet);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Hand);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Head);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			ScrollView.Instance.Add (armorInfo.type.ToString(), () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.Legs);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			ScrollView.Instance.Add ("left hand", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.LeftRing);
				ShowInventory();
				ScrollView.Instance.Add ("You equip " + info.name + " on left hand");
			});
			ScrollView.Instance.Add (" or ");
			ScrollView.Instance.Add ("right hand\n", () => {
				Game.Instance.player.EquipArmorItem (slot.index, Character.ArmorPart.RightRing);
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
				Game.Instance.player.EquipWeaponItem (slot.index);	
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
			Game.Instance.player.inventory.Pull(slot.index);
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
}
