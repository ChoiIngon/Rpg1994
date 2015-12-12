using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class InventoryView : MonoBehaviour {
	public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
	public SlotView slotViewPref;
	[HideInInspector]
	public SlotView selectedSlotView;

	public Transform slots;
	public Button[] buttons;
	public Text weight;
	public Text gold;
	void Start() {
		for (int i=0; i<Inventory.MAX_SLOT_COUNT; i++) {
			SlotView slotView = Instantiate<SlotView>(slotViewPref);
			slotView.transform.SetParent(slots, false);
		}
		selectedSlotView = null;
	}
}
