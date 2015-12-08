using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class InventoryView : Util.UI.Singleton<InventoryView> {
	public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
	public SlotView slotViewPref;
	[HideInInspector]
	public SlotView selectedSlotView;
	public GameObject selectBox;

	public Transform slots;
	public Text gold;
	public Text weight;
	public Text description;
	public Button[] buttons;

	public void Init() {
		/*
		Sprite[] s = Resources.LoadAll<Sprite> ("Image/items");
		for (int i=0; i<s.Length; i++) {
			sprites.Add (s[i].name, s[i]);
		}
		*/
		for (int i=0; i<Inventory.MAX_SLOT_COUNT; i++) {
			SlotView slotView = Instantiate<SlotView>(slotViewPref);
			slotView.transform.SetParent(slots, false);
		}
		transform.localScale = new Vector3 (0, 0, 0);
		gameObject.SetActive (false);
		selectedSlotView = null;
	}
		
	private void Reset()
	{
		selectBox.gameObject.SetActive (false);
		selectedSlotView = null;
		description.text = "";
		for(int i=0; i<(int)SlotView.SlotActionType.Max; i++)
		{
			buttons[i].gameObject.SetActive(false);
		}
		global::Inventory inventory = GameManager.Instance.player.inventory;
		for (int i=0; i<inventory.slots.Length; i++) {
			global::Inventory.Slot slot = inventory.slots[i];
			if(null == slot)
			{
				slots.GetChild(i).gameObject.SetActive(false);
				continue;
			}
			SlotView slotView = slots.GetChild(i).GetComponent<SlotView>();
			slotView.Init (slot);
			slotView.gameObject.SetActive(true);
		}

		gold.text = "gold : " + GameManager.Instance.player.inventory.gold;
		weight.text = "weight : " + GameManager.Instance.player.inventory.weight + "/" + 
			GameManager.Instance.player.inventory.maxWeight;
	}
	public void OnOpen()
	{
		Reset ();
		gameObject.SetActive (true);
		gameObject.GetComponent<Animation>().Play();
	}

	public void OnSelect(int index)
	{
		selectBox.gameObject.SetActive (true);
		selectedSlotView = slots.GetChild(index).GetComponent<SlotView>();
		selectBox.transform.position = selectedSlotView.transform.position;
		global::Inventory inventory = GameManager.Instance.player.inventory;
		global::Inventory.Slot slot = inventory.slots[index];

		description.text = "<b>[" + slot.item.info.name + "]</b> ";
		description.text += "weight : " + slot.item.info.weight + ", cost : " + slot.item.info.cost + "\n\n";
		description.text += slot.item.info.description;

		for(int i=0; i<(int)SlotView.SlotActionType.Max; i++)
		{
			if(true == selectedSlotView.availableAction[i])
			{
				buttons[i].gameObject.SetActive(true);
			}
			else
			{
				buttons[i].gameObject.SetActive(false);
			}
		}
	}

	public void OnEquip()
	{
		selectedSlotView.OnEquip ();
		Reset ();
	}

	public void OnEquipLeftRing()
	{
		selectedSlotView.OnEquipLeftRing ();
		Reset ();
	}
	public void OnEquipRightRing()
	{
		selectedSlotView.OnEquipRightRing ();
		Reset ();
	}
	public void OnUse()
	{
		selectedSlotView.OnUse ();
		Reset ();
	}
	public void OnDrop()
	{
		selectedSlotView.OnDrop ();
		Reset ();
	}
	public void OnClose()
	{
		transform.localScale = new Vector3 (0, 0, 0);
		gameObject.SetActive (false);
	}
}
