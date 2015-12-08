using UnityEngine;
using System.Collections;

public class Inventory
{
	public class Slot
	{
		public int index;
		public int count;
		public ItemData item;
	};
	public const int MAX_SLOT_COUNT = 24;
	public int weight;
	public int maxWeight;
	public int gold;
	public Slot[] slots = new Slot[MAX_SLOT_COUNT];
	
	public void Put(ItemData data)
	{
		if(maxWeight < weight + data.info.weight)
		{
			throw new System.Exception("over burdden");
		}
		switch (data.info.category) {
		case ItemInfo.Category.Potion:
			foreach(Slot slot in slots)
			{
				if(null == slot) {
					continue;
				}
				if(data.info.id == slot.item.info.id)
				{
					slot.count += 1;
					weight += data.info.weight;
					return;
				}
			}
			break;
		}
		
		for(int i=0;i<slots.Length; i++)
		{
			Slot slot = slots[i];
			if(null == slot)
			{
				slots[i] = new Slot();
				slots[i].item = data;
				slots[i].index = i;
				slots[i].count = 1;
				weight += data.info.weight;
				return;
			}
		}
		throw new System.Exception ("no more room in inventory");
	}
	
	public ItemData Pull(int index)
	{
		Slot slot = slots [index];
		if (null == slot) {
			throw new System.Exception("no item");
		}
		
		ItemData item = slot.item;
		weight -= item.info.weight;
		
		slot.count -= 1;
		if (0 == slot.count) {
			slots [index] = null;
		}
		return item;
	}
}