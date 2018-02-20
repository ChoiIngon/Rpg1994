using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardInfo {
	public Util.RangeInt gold = new Util.RangeInt();
	public Util.RangeInt exp = new Util.RangeInt();
	public List<ItemInfo> items = new List<ItemInfo> ();

	public RewardData CreateInstance() {
		RewardData data = new RewardData ();
		data.gold = gold;
		data.exp = exp;
		foreach (ItemInfo item in items) {
			if(UnityEngine.Random.Range(1, 10) <= 3)
			{
				//data.items.Add(ItemManager.Instance.CreateInstance(item.id));
			}
		}
		return data;
	}
}

public class RewardData {
	public int gold = 0;
	public int exp = 0;
	public List<ItemData> items = new List<ItemData> ();
}

