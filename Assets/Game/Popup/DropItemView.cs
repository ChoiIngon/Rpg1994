using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropItemView : Util.UI.Singleton<DropItemView> {
	public ItemStackView itemStackViewPref;
	public Transform itemStacks;
	public void Start()
	{
		gameObject.SetActive (false);
	}

	public void Init(List<ItemStack> stacks)
	{
		Clear ();
		foreach (ItemStack stack in stacks) {
			Add (stack);
		}
	}

	private void Clear()
	{
		for (int i=0; i<itemStacks.childCount; i++) {
			GameObject.Destroy(itemStacks.GetChild(i).gameObject);
		}
	}
	
	private void Add(ItemStack stack)
	{
		ItemStackView stackView = Instantiate<ItemStackView>(itemStackViewPref);
		stackView.Init (stack);
		stackView.transform.SetParent (itemStacks, false);
	}

	public void OnPickup() {
		if(0 == itemStacks.childCount) {
			gameObject.SetActive(false);
		}
	}
}
