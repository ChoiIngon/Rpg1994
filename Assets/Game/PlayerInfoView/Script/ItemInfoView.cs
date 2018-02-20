using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInfoView : Util.UI.Singleton<ItemInfoView> {
	/*
	public enum ButtonType
	{
		Equip,
		EquipLeftRing,
		EquipRightRing,
		Unequip,
		Use,
		Drop,
		Max
	};
	public Text itemName;
	public Image image;
	public Text description;

	public AttributeView cost;
	public AttributeView weight;
	public AttributeView count;
	public Button[] buttons;

	private SlotView slot;
	private EquipmentView equipment;
	private ItemData item;
	// Use this for initialization
	void Start () {
		transform.localPosition = Vector3.zero;
		gameObject.SetActive (false);
	}

	public void Init(SlotView slot)
	{
		this.slot = slot;
		this.equipment = null;
		count.Value = slot.slot.count.ToString ();
		foreach (Button button in buttons) {
			button.gameObject.SetActive(false);
		}
		ItemInfo info = slot.slot.item.info;
		switch (info.category) {
		case ItemInfo.Category.Weapon:
			buttons[(int)ButtonType.Equip].gameObject.SetActive(true);
			this.slot.equipPart = Character.EquipPart.Weapon;
			break;
		case ItemInfo.Category.Ring:
			buttons[(int)ButtonType.EquipLeftRing].gameObject.SetActive(true);
			buttons[(int)ButtonType.EquipRightRing].gameObject.SetActive(true);
			break;
		case ItemInfo.Category.Shield :
			buttons[(int)ButtonType.Equip].gameObject.SetActive(true);
			this.slot.equipPart = Character.EquipPart.Shield;
			break;
		case ItemInfo.Category.Shirt : 
			buttons[(int)ButtonType.Equip].gameObject.SetActive(true);
			this.slot.equipPart = Character.EquipPart.Shirt;
			break;
		case ItemInfo.Category.Potion:
			buttons[(int)ButtonType.Use].gameObject.SetActive(true);
			break;
		}
		Init (this.slot.slot.item);	
	}

	public void Init(EquipmentView equipment)
	{
		this.equipment = equipment;
		this.slot = null;
		count.Value = "1";
		foreach (Button button in buttons) {
			button.gameObject.SetActive(false);
		}
		buttons [(int)ButtonType.Unequip].gameObject.SetActive (true);
		Init (equipment.equipment);
	}

	private void Init(ItemData data)
	{
		item = data;
		itemName.text = data.info.name;
		buttons [(int)ButtonType.Drop].gameObject.SetActive (true);
		image.sprite = Resources.Load<Sprite> ("Texture/Item/"+data.info.id);
		weight.Value = data.info.weight.ToString();
		cost.Value = data.info.cost.ToString ();
		description.text = data.info.description;
		gameObject.SetActive (true);
	}

	public void OnClose()
	{
		gameObject.SetActive (false);
	}

	public void OnEquip()
	{
		Player.Instance.EquipItem (slot.slot.index, slot.equipPart);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnEquipLeftRing()
	{
		Player.Instance.EquipItem (slot.slot.index, Character.EquipPart.LeftRing);	
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnEquipRightRing()
	{
		Player.Instance.EquipItem (slot.slot.index, Character.EquipPart.RightRing);	
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnUnequip()
	{
		if (null == equipment) {
			throw new System.Exception("select item first");
		}
		Player.Instance.UnequipItem (equipment.equipment.part);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
	}

	public void OnUse()
	{
		if (null == slot) {
			throw new System.Exception ("select item first");
		}
		Player.Instance.UseItem (slot.slot.index);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
	}

	public void OnDrop()
	{
		if (null != slot) {
			Player.Instance.DropItem(slot.slot.index);
			PlayerInfoView.Instance.Init ();
			gameObject.SetActive (false);
			return;
		}

		if (null != equipment) {
			Player.Instance.DropItem(equipment.equipment.part);
			PlayerInfoView.Instance.Init ();
			gameObject.SetActive (false);
			return;
		}

		throw new System.Exception("select item first");
	}
	*/
}
