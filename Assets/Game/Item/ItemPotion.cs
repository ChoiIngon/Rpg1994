using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotionItemInfo : ItemInfo {
	public List<BuffInfo> buff = new List<BuffInfo> ();
	public PotionItemInfo() {
		category = ItemInfo.Category.Potion;
	}
	/*
	public override ItemData CreateInstance() {
		PotionItemData data = new PotionItemData ();
		data.info = this;
		return data;
	}
	*/
}

public class PotionItemData : ItemData {
	/*
	public override Character.Status Use (Character character) {
		Character.Status state = new Character.Status ();
		PotionItemInfo info = (PotionItemInfo)this.info;
		foreach (BuffInfo buffInfo in info.buff) {
			BuffData buffData = buffInfo.CreateInstance ();
			state += buffData.ApplyBuff (character);
			if (true == buffData.IsValid ()) {
				character.buffs.Add (buffData);
			}
		}
		return state;
	}
	*/
}