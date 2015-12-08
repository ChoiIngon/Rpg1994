using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfoView : Util.UI.Singleton<PlayerInfoView> { 
	public EquipmentView[] equipmentViews = new EquipmentView[(int)Character.EquipPart.Max];
	public InventoryView inventoryView;
	public Text level;
	public Text health;
	public Text mana;
	public Text attack;
	public Text defense;
	public Text speed;

	void Start () {
		gameObject.SetActive (false);
		//transform.localScale = new Vector3 (0, 0, 0);
		inventoryView = transform.FindChild ("Background/InventoryView").GetComponent<InventoryView> ();
		equipmentViews[(int)Character.EquipPart.Weapon] = transform.FindChild ("Background/Avata/Weapon").GetComponent<EquipmentView> ();
		equipmentViews[(int)Character.EquipPart.Shield] = transform.FindChild("Background/Avata/Shield").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.LeftRing] = transform.FindChild("Background/Avata/LeftRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.RightRing] = transform.FindChild("Background/Avata/RightRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.Shirt] = transform.FindChild("Background/Avata/Shirt").GetComponent<EquipmentView>();

		level = transform.FindChild ("Background/Status/Level/Value").GetComponent<Text> ();
		health = transform.FindChild ("Background/Status/Health/Value").GetComponent<Text> ();
		mana = transform.FindChild ("Background/Status/Mana/Value").GetComponent<Text> ();
		attack = transform.FindChild ("Background/Status/Attack/Value").GetComponent<Text> ();
		defense = transform.FindChild ("Background/Status/Defense/Value").GetComponent<Text> ();
		speed = transform.FindChild ("Background/Status/Speed/Value").GetComponent<Text> ();
	}

	public void OnOpen()
	{
		gameObject.SetActive (true);
		Init ();
	}

	public void OnClose()
	{
		//transform.localScale = new Vector3 (0, 0, 0);
		gameObject.SetActive (false);
	}

	public void Init()
	{
		InitStatus ();
		InitEquipment ();
		InitInventory ();
	}

	private void InitStatus()
	{
		Character.Status status = GameManager.Instance.player.GetStatus ();
		level.text = "1";
		health.text = string.Format("{0}/{1}", status.health, GameManager.Instance.player.health.max);
		mana.text = string.Format("{0}/{1}", 0, 0);
		attack.text = status.attack.ToString();
		defense.text = status.defense.ToString ();
		speed.text = status.speed.ToString ();
	}
	private void InitEquipment()
	{
		EquipmentItemData [] items = GameManager.Instance.player.items;
		for (int i=0; i<items.Length; i++) {
			EquipmentItemData item = items[i];
			if(null == item)
			{
				equipmentViews[i].gameObject.SetActive(false);
				continue;
			}
			equipmentViews[i].gameObject.SetActive(true);
			equipmentViews[i].Init (item);
		}
	}
	private void InitInventory()
	{
		Transform slots = inventoryView.transform.FindChild ("Slots");

		for (int i=0; i<GameManager.Instance.player.inventory.slots.Length; i++) {
			global::Inventory.Slot slot = GameManager.Instance.player.inventory.slots[i];
			if(null == slot)
			{
				slots.GetChild(i).gameObject.SetActive(false);
				continue;
			}
			SlotView slotView = slots.GetChild(i).GetComponent<SlotView>();
			slotView.Init (slot);
			slotView.gameObject.SetActive(true);
		}
	}
}
