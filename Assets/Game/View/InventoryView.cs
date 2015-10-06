using UnityEngine;
using System.Collections;

public class InventoryView {
	Inventory inventory;
	public InventoryView(Inventory inventory) {
		this.inventory = inventory;
		ShowInventory ();
	}
	public void ShowArmorInventorySlot(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		ArmorItemInfo armorInfo = (ArmorItemInfo)info;
		switch (armorInfo.type) {
		case ArmorItemInfo.ItemType.Body :
			LogView.Button ("[<color=blue>입기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Body);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Feet :
			LogView.Button ("[<color=blue>신기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Feet);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Hand :
			LogView.Button ("[<color=blue>끼기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Hand);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Head :
			LogView.Button ("[<color=blue>쓰기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Head);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Legs :
			LogView.Button ("[<color=blue>입기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Neck :
			LogView.Button ("[<color=blue>걸기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Legs);
				ShowInventory();
				LogView.Text ("You equip " + info.name + " on " + armorInfo.type.ToString() + "\n");
			});
			break;
		case ArmorItemInfo.ItemType.Ring :
			LogView.Instance.Add ("[<color=blue>왼손에 끼기</color>]", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.LeftRing);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on left hand");
			});
			LogView.Instance.Add (", ");
			LogView.Instance.Add ("[<color=blue>오른손에 끼기</color>] ", () => {
				Game.Instance.player.EquipItem (slot.index, Character.EquipPart.RightRing);
				ShowInventory();
				LogView.Instance.Add ("You equip " + info.name + " on right hand\n");
			});
			break;
		}
	}
	public void ShowInventorySlot(Inventory.Slot slot) {
		ItemInfo info = slot.item.info;
		LogView.Text("weight:" + info.weight + ", cost:" + info.cost + "\n");
		LogView.Text(info.description + "\n");
	}
	
	public void ShowInventory(int slotIndex = -1) {
		LogView.Title ("인벤토리");
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
				LogView.Text("<b><color=red>" + type + name + " " + slot.count + "</color></b>\n");
				ShowInventorySlot (slot);
			}
			else {
				LogView.Button (type + name + " " + slot.count + "\n", () => {
					ShowInventory(slot.index);
				});
			}
		}

		if (0 <= slotIndex && null != inventory.slots[slotIndex]) {
			LogView.Bar ();
			Inventory.Slot slot = inventory.slots[slotIndex];
			ItemInfo info = slot.item.info;
			LogView.Instance.Add (info.name + "을(를) ");
			switch (info.category) {
			case ItemInfo.Category.Weapon:
				LogView.Instance.Add ("[<color=blue>장착</color>] ", () => { 
					Game.Instance.player.EquipItem (slot.index, Character.EquipPart.Weapon);	
					ShowInventory();
					LogView.Instance.Add ("You equip " + info.name + "\n");
				});
				break;
			case ItemInfo.Category.Armor:
				ShowArmorInventorySlot(slot);
				break;
			case ItemInfo.Category.Potion:
				LogView.Instance.Add ("[<color=yellow>마시기</color>] ", () => { 
					Game.Instance.player.UseItem (slot.index);	
					ShowInventory();
					LogView.Text ("You drink " + info.name + "\n");
				});
				break;
			}
			LogView.Text ("또는 ");
			LogView.Button ("[<color=red>버리기</color>]\n", () => {
				ItemData item = Game.Instance.player.inventory.Pull(slot.index);
				ItemStack itemStack = Game.Instance.player.CreateItemStack(item, Game.Instance.player.position);
				//OnDropItem(itemStack);
				ShowInventory();
				LogView.Text ("You drop " + info.name + "\n");
			});
		}
	}
	public void ShowEquipItemInfo(Character.EquipPart part, ItemData item) {
		ItemInfo info = item.info;
		LogView.Title (item.info.name);
		LogView.Text ("weight:" + info.weight + ", cost:" + info.cost + "\n");
		LogView.Text (info.description + "\n");
		LogView.Text ("\n");
		LogView.Text ("You can ");
		
		LogView.Button ("unequip", () => { 
			Game.Instance.player.UnequipItem(part);
			LogView.Text ("You unequiped " + info.name + "\n");
		});
		LogView.Text (" or ");
		LogView.Button ("drop\n", () => {
			ItemStack itemStack = Game.Instance.player.DropItem(part);
			//OnDropItem(itemStack);
			LogView.Text ("You droped " + info.name + "\n");
		});
	} 
	public void OnPickupItem(ItemData item) {
		LogView.Text ("You picked " + item.info.name + " up\n");
	}
}
