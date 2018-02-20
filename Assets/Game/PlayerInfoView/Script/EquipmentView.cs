using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentView : MonoBehaviour {
	// Use this for initialization
	public EquipmentItemData equipment;
	public void Init(EquipmentItemData item)
	{
		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Texture/Item/"+item.info.id);
		equipment = item;
	}
	public void OnClick()
	{
		if (null == equipment) {
			return;
		}
		//ItemInfoView.Instance.Init (this);
	}
}
