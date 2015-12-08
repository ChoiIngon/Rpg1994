using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SlotView : MonoBehaviour {
	private Inventory.Slot slot;
	public Character.EquipPart equipPart;
	public enum SlotActionType
	{
		Equip,
		Equip_LeftHand,
		Equip_RightHand,
		Use,
		Drop,
		Max
	};
	public bool[] availableAction = new bool[(int)SlotActionType.Max];
	public void Init(Inventory.Slot slot)
	{
		for(int i=0; i<(int)SlotView.SlotActionType.Max; i++)
		{
			availableAction[i] = false;
		}
		this.slot = slot;

		Sprite sprite = Resources.Load<Sprite> ("Texture/Item/"+slot.item.info.id);
		GetComponent<Image> ().sprite = sprite;
		Transform count = transform.FindChild ("Count");
		count.GetComponent<Text> ().text = slot.count.ToString ();

		ItemInfo info = slot.item.info;
		switch (info.category) {
		case ItemInfo.Category.Weapon:
			availableAction[(int)SlotActionType.Equip] = true;
			equipPart = Character.EquipPart.Weapon;
			break;
		case ItemInfo.Category.Ring:
			availableAction[(int)SlotActionType.Equip_LeftHand] = true;
			availableAction[(int)SlotActionType.Equip_RightHand] = true;
			break;
		case ItemInfo.Category.Shield :
			availableAction[(int)SlotActionType.Equip] = true;
			equipPart = Character.EquipPart.Shield;
			break;
		case ItemInfo.Category.Shirt : 
			availableAction[(int)SlotActionType.Equip] = true;
			equipPart = Character.EquipPart.Shirt;
			break;
		case ItemInfo.Category.Potion:
			availableAction[(int)SlotActionType.Use] = true;
			break;
		}
		availableAction[(int)SlotActionType.Drop] = true;
	}
	public void OnClick()
	{
		InventoryView.Instance.OnSelect (slot.index);
	}
	public void OnEquip()
	{
		GameManager.Instance.player.EquipItem (slot.index, equipPart);	
		LogView.Text ("You equiped " + slot.item.info.name + "\n");
	}
	public void OnEquipLeftRing()
	{
		GameManager.Instance.player.EquipItem (slot.index, Character.EquipPart.LeftRing);	
		LogView.Text ("You equiped " + slot.item.info.name + "\n");
	}
	public void OnEquipRightRing()
	{
		GameManager.Instance.player.EquipItem (slot.index, Character.EquipPart.LeftRing);	
		LogView.Text ("You equiped " + slot.item.info.name + "\n");
	}
	public void OnUse()
	{
		GameManager.Instance.player.UseItem (slot.index);
	}
	public void OnDrop()
	{
		ItemData item = GameManager.Instance.player.inventory.Pull(slot.index);
		//ItemStack itemStack = 
		GameManager.Instance.player.CreateItemStack(item, GameManager.Instance.player.position);
	}
}
