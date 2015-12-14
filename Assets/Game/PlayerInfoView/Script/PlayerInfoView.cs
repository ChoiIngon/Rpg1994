using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfoView : Util.UI.Singleton<PlayerInfoView> { 
	public EquipmentView[] equipmentViews = new EquipmentView[(int)Character.EquipPart.Max];
	public Transform inventorySlots;
	public SlotView slotViewPref;
	public AttributeView level;
	public AttributeView health;
	public AttributeView stamina;
	public AttributeView attack;
	public AttributeView defense;
	public AttributeView speed;

	void Start () {
		gameObject.SetActive (false);
		//transform.localScale = new Vector3 (0, 0, 0);
		equipmentViews[(int)Character.EquipPart.Weapon] = transform.FindChild ("Background/Content/Avata/Weapon").GetComponent<EquipmentView> ();
		equipmentViews[(int)Character.EquipPart.Shield] = transform.FindChild("Background/Content/Avata/Shield").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.LeftRing] = transform.FindChild("Background/Content/Avata/LeftRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.RightRing] = transform.FindChild("Background/Content/Avata/RightRing").GetComponent<EquipmentView>();
		equipmentViews[(int)Character.EquipPart.Shirt] = transform.FindChild("Background/Content/Avata/Shirt").GetComponent<EquipmentView>();

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
		Character.Status status = GameManager.Instance.player.GetStatus ();
		level.Value = "1";
		health.Value = string.Format("{0}/{1}", status.health, GameManager.Instance.player.health.max);
		stamina.Value = string.Format ("{0}/{1}", status.stamina, GameManager.Instance.player.stamina.max);
		attack.Value = status.attack.ToString();
		defense.Value = status.defense.ToString ();
		speed.Value = status.speed.ToString ();
	}
	private void InitEquipment()
	{
		EquipmentItemData [] equipments = GameManager.Instance.player.equipments;
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
		for (int i=0; i<GameManager.Instance.player.inventory.slots.Length; i++) {
			global::Inventory.Slot slot = GameManager.Instance.player.inventory.slots[i];
			if(null == slot)
			{
				inventorySlots.GetChild(i).gameObject.SetActive(false);
				continue;
			}
			SlotView slotView = inventorySlots.GetChild(i).GetComponent<SlotView>();
			slotView.Init (slot);
			slotView.gameObject.SetActive(true);
		}

		//inventoryView.gold.text = "gold : " + GameManager.Instance.player.inventory.gold.ToString();
		//inventoryView.weight.text = "weight : " + GameManager.Instance.player.inventory.weight.ToString () + "/" + GameManager.Instance.player.inventory.maxWeight.ToString();
	}
}
