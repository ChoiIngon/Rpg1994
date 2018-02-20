using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfoView : Util.UI.Singleton<PlayerInfoView> { 
	/*
	public EquipmentView[] equipmentViews = new EquipmentView[(int)Character.EquipPart.Max];
	public Transform inventorySlots;
	public SlotView slotViewPref;
	public AttributeView level;
	public AttributeView health;
	public AttributeView stamina;
	public AttributeView attack;
	public AttributeView defense;
	public AttributeView speed;
	public AttributeView gold;
	public AttributeView weight;

	void Start () {
		transform.localPosition = Vector3.zero;
		gameObject.SetActive (false);
		//transform.localScale = new Vector3 (0, 0, 0);
		equipmentViews[(int)Character.EquipPart.Weapon] = transform.Find ("Background/Content/Avata/Content/Weapon").GetComponent<EquipmentView> ();
		equipmentViews[(int)Character.EquipPart.Shield] = transform.Find("Background/Content/Avata/Content/Shield").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.LeftRing] = transform.Find("Background/Content/Avata/Content/LeftRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.RightRing] = transform.Find("Background/Content/Avata/Content/RightRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.Shirt] = transform.Find("Background/Content/Avata/Content/Shirt").GetComponent<EquipmentView>();

		for (int i=0; i<Inventory.MAX_SLOT_COUNT; i++) {
			SlotView slotView = Instantiate<SlotView>(slotViewPref);
			slotView.transform.SetParent(inventorySlots, false);
		}
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
		Character.Status status = Player.Instance.GetStatus ();
		level.Value = "1";
		health.Value = string.Format("{0}/{1}", status.health, Player.Instance.health.max);
		stamina.Value = string.Format ("{0}/{1}", status.stamina, Player.Instance.stamina.max);
		attack.Value = status.attack.ToString();
		defense.Value = status.defense.ToString ();
		speed.Value = status.speed.ToString ();
	}
	private void InitEquipment()
	{
		EquipmentItemData [] equipments = Player.Instance.equipments;
		for (int i=0; i<equipments.Length; i++) {
			EquipmentItemData item = equipments[i];
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
		for (int i=0; i<Player.Instance.inventory.slots.Length; i++) {
			global::Inventory.Slot slot = Player.Instance.inventory.slots[i];
			if(null == slot)
			{
				inventorySlots.GetChild(i).gameObject.SetActive(false);
				continue;
			}
			SlotView slotView = inventorySlots.GetChild(i).GetComponent<SlotView>();
			slotView.Init (slot);
			slotView.gameObject.SetActive(true);
		}

		gold.Value = Player.Instance.inventory.gold.ToString();
		weight.Value = Player.Instance.inventory.weight.ToString () + "/" + Player.Instance.inventory.maxWeight.ToString();
	}
	*/
}
