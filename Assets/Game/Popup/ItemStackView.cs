using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemStackView : MonoBehaviour {
	private ItemStack stack;

	public void Init(ItemStack stack)
	{
		this.stack = stack;
		Image image = transform.FindChild ("Image").GetComponent<Image>();
		Text name = transform.FindChild ("Name").GetComponent<Text> ();
		image.sprite = Resources.Load<Sprite> ("Texture/Item/"+ stack.item.info.id);
		name.text = stack.item.info.name;
	}

	public void OnPickup() {
		GameManager.Instance.player.PickupItem (stack);
		transform.SetParent (null);
		GameObject.Destroy (gameObject);
		DropItemView.Instance.OnPickup ();
	}
}
