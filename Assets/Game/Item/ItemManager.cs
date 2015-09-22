using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class ItemManager : SingletonObject<ItemManager> {
	private Dictionary<string, ItemInfo> dictItemInfo = new Dictionary<string, ItemInfo>();
	public ItemManager() {
		Init ();
	}

	public void Init() {
		TextAsset resource = Resources.Load ("ItemInfo") as TextAsset;
		JsonData root = JsonMapper.ToObject (resource.text);

		InitWeaponItemInfo(root);
		InitArmorItemInfo(root);
		InitPotionItemInfo(root);
		InitKeyItemInfo (root);
	}
	private void InitWeaponItemInfo(JsonData root)
	{
		JsonData itemInfos = root ["weapon"];
		for (int i=0; i<itemInfos.Count; i++) {
			JsonData jsonInfo = itemInfos[i];
			WeaponItemInfo itemInfo = new WeaponItemInfo();
			itemInfo.id = (string)jsonInfo["id"];
			itemInfo.name = (string)jsonInfo["name"];
			itemInfo.cost = (int)jsonInfo["cost"];
			itemInfo.weight = (int)jsonInfo["weight"];
			itemInfo.description = (string)jsonInfo["description"];
			itemInfo.attack.SetValue((string)jsonInfo["attack"]);
			itemInfo.speed = (int)jsonInfo["speed"];
			itemInfo.category = ItemInfo.Category.Weapon;
			itemInfo.type = WeaponItemInfo.ToType((string)jsonInfo["type"]);
			dictItemInfo.Add (itemInfo.id, itemInfo);
		}
	}
	private void InitArmorItemInfo(JsonData root)
	{
		JsonData itemInfos = root ["armor"];
		for (int i=0; i<itemInfos.Count; i++) {
			JsonData jsonInfo = itemInfos[i];
			ArmorItemInfo itemInfo = new ArmorItemInfo();
			itemInfo.id = (string)jsonInfo["id"];
			itemInfo.name = (string)jsonInfo["name"];
			itemInfo.cost = (int)jsonInfo["cost"];
			itemInfo.weight = (int)jsonInfo["weight"];
			itemInfo.description = (string)jsonInfo["description"];
			itemInfo.defense = (int)jsonInfo["defense"];
			itemInfo.speed = (int)jsonInfo["speed"];
			itemInfo.category = ItemInfo.Category.Armor;
			itemInfo.type = ArmorItemInfo.ToType((string)jsonInfo["type"]);
			dictItemInfo.Add (itemInfo.id, itemInfo);
		}
	}

	private delegate BuffInfo InitBuffInfo(JsonData attr);
	private void InitPotionItemInfo(JsonData root)
	{
		Dictionary<string, InitBuffInfo> initBuffInfo = new Dictionary<string, InitBuffInfo> ();
		initBuffInfo ["heal"] = (JsonData attr) => { 
			HealBuffInfo info = new HealBuffInfo ();
			info.name = (string)attr ["name"];
			info.amount = (int)attr ["amount"];
			return info;
		};
		initBuffInfo ["attack"] = (JsonData attr) => { 
			AttackBuffInfo info = new AttackBuffInfo();
			info.name = (string)attr ["name"];
			info.attack = (int)attr["attack"];
			info.turn = (int)attr["turn"];
			return info;
		};
		initBuffInfo ["poison"] = (JsonData attr) => { 
			PoisonBuffInfo info = new PoisonBuffInfo();
			info.name = (string)attr ["name"];
			info.damage.SetValue((string)attr["damage"]);
			info.turn = (int)attr["turn"];
			return info;
		};
		JsonData itemInfos = root ["potion"];
		for (int i=0; i<itemInfos.Count; i++) {
			JsonData jsonInfo = itemInfos[i];
			PotionItemInfo itemInfo = new PotionItemInfo();
			itemInfo.id = (string)jsonInfo["id"];	
			itemInfo.name = (string)jsonInfo["name"];
			itemInfo.cost = (int)jsonInfo["cost"];
			itemInfo.weight = (int)jsonInfo["weight"];
			itemInfo.description = (string)jsonInfo["description"];
			itemInfo.category = ItemInfo.Category.Potion;
			JsonData jsonBuff = jsonInfo["buff"];
			itemInfo.buff.Add (initBuffInfo[(string)jsonBuff["type"]](jsonBuff));
			dictItemInfo.Add (itemInfo.id, itemInfo);
		}
	}
	private void InitKeyItemInfo(JsonData root) {
		JsonData itemInfos = root ["key"];
		for (int i=0; i<itemInfos.Count; i++) {
			JsonData jsonInfo = itemInfos[i];
			KeyItemInfo itemInfo = new KeyItemInfo();
			itemInfo.id = (string)jsonInfo["id"];
			itemInfo.name = (string)jsonInfo["name"];
			itemInfo.cost = (int)jsonInfo["cost"];
			itemInfo.weight = (int)jsonInfo["weight"];
			itemInfo.description = (string)jsonInfo["description"];
			itemInfo.category = ItemInfo.Category.Key;
			dictItemInfo.Add (itemInfo.id, itemInfo);
		}
	}
	public ItemInfo Find(string id) {
		return dictItemInfo [id];
	}
	public ItemData CreateInstance(string id) {
		return dictItemInfo [id].CreateInstance ();
	}
}
