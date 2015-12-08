using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemView : Util.UI.Singleton<ItemView> {
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
	public Image image;
	public Text description;
	public Button[] buttons;

	private SlotView slot;
	private EquipmentView equipment;
	private ItemData item;
	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		image = transform.FindChild ("Background/Image").GetComponent<Image> ();
		buttons = new Button[(int)ButtonType.Max];
		buttons [(int)ButtonType.Equip] = transform.FindChild ("Background/Buttons/Equip").GetComponent<Button>();
		buttons [(int)ButtonType.EquipLeftRing] = transform.FindChild ("Background/Buttons/EquipLeftRing").GetComponent<Button>();
		buttons [(int)ButtonType.EquipRightRing] = transform.FindChild ("Background/Buttons/EquipRightRing").GetComponent<Button>();
		buttons [(int)ButtonType.Unequip] = transform.FindChild ("Background/Buttons/Unequip").GetComponent<Button>();
		buttons [(int)ButtonType.Use] = transform.FindChild ("Background/Buttons/Use").GetComponent<Button>();
		buttons [(int)ButtonType.Drop] = transform.FindChild ("Background/Buttons/Drop").GetComponent<Button>();
		description = transform.FindChild ("Background/Description/Text").GetComponent<Text> ();
	}

	public void Init(SlotView slot)
	{
		this.slot = slot;
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
		foreach (Button button in buttons) {
			button.gameObject.SetActive(false);
		}
		buttons [(int)ButtonType.Unequip].gameObject.SetActive (true);
		Init (equipment.equipment);
	}

	private void Init(ItemData data)
	{
		item = data;
		buttons [(int)ButtonType.Drop].gameObject.SetActive (true);
		image.sprite = Resources.Load<Sprite> ("Texture/Item/"+data.info.id);
		description.text = data.info.description;
		gameObject.SetActive (true);
	}

	public void OnClose()
	{
		gameObject.SetActive (false);
	}

	public void OnEquip()
	{
		GameManager.Instance.player.EquipItem (slot.slot.index, slot.equipPart);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnEquipLeftRing()
	{
		GameManager.Instance.player.EquipItem (slot.slot.index, Character.EquipPart.LeftRing);	
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnEquipRightRing()
	{
		GameManager.Instance.player.EquipItem (slot.slot.index, Character.EquipPart.RightRing);	
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
		LogView.Instance.Write ("You equiped " + item.info.name);
	}

	public void OnUnequip()
	{
		if (null == equipment) {
			throw new System.Exception("select item first");
		}
		GameManager.Instance.player.UnequipItem (equipment.equipment.part);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
	}

	public void OnUse()
	{
		if (null == slot) {
			throw new System.Exception ("select item first");
		}
		GameManager.Instance.player.UseItem (slot.slot.index);
		PlayerInfoView.Instance.Init ();
		gameObject.SetActive (false);
	}

	public void OnDrop()
	{
		if (null != slot) {
			GameManager.Instance.player.DropItem(slot.slot.index);
			PlayerInfoView.Instance.Init ();
			gameObject.SetActive (false);
			return;
		}

		if (null != equipment) {
			GameManager.Instance.player.DropItem(equipment.equipment.part);
			PlayerInfoView.Instance.Init ();
			gameObject.SetActive (false);
			return;
		}

		throw new System.Exception("select item first");
	}
}
