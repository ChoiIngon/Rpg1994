using UnityEngine;
using UnityEditor;
using System.Collections;

public class CharacterView : ObjectView {
	public void OnAttack(Character defender, int damage) {
		Character attacker = (Character)targetObject;
		if (0 < damage) {
			ScrollView.Instance.Add (attacker.name, () => { 
				((CharacterView)attacker.view).ShowCharacterInfo();
			});
			
			ScrollView.Instance.Add (" damaged " + damage + " to ");
			ScrollView.Instance.Add (defender.name + "\n", () => { 
				((CharacterView)defender.view).ShowCharacterInfo();
			});
		} else {
			ScrollView.Instance.Add (defender.name, () => { 
				((CharacterView)attacker.view).ShowCharacterInfo();
			});
			ScrollView.Instance.Add (" dodge!!\n");
		}
	}
	public void OnDetachBuff(BuffData buff) {
		Character character = (Character)targetObject;
		ScrollView.Instance.Add (character.name, () => {
			((CharacterView)character.view).ShowCharacterInfo();
		});
		ScrollView.Instance.Add ("'s " + buff.info.name + " is expired");
	}
	public void ShowAttack() {
		Character character = (Character)targetObject;
		string item = "0";
		if (null != character.items[(int)Character.EquipPart.Weapon]) {
			WeaponItemInfo info = (WeaponItemInfo)character.items[(int)Character.EquipPart.Weapon].info;
			item = info.attack.ToString();
		}
		int buff = 0;
		for (int i=0; i<character.buffs.Count; i++) {
			if (true == character.buffs[i].IsValid ()) {
				buff += character.buffs[i].ApplyBuff (character).attack;
			}
		}
		ScrollView.Instance.Add ("attack:" + character.attack + " + " + item + " + " + buff + "\n");
	}
	public void ShowDefense() {
		Character character = (Character)targetObject;
		int item = 0;
		for (int i=0; i<(int)character.items.Length; i++) {
			if(null != character.items[i]) {
				if(ItemInfo.Category.Armor != character.items[i].info.category)	{
					continue;
				}
				ArmorItemInfo info = (ArmorItemInfo)character.items[i].info;
				if(null != info) {
					item += info.defense;
				}
			}
		}
		int buff = 0;
		for (int i=0; i<character.buffs.Count; i++) {
			if (true == character.buffs[i].IsValid ()) {
				buff += character.buffs[i].ApplyBuff (character).defense;
			}
		}
		ScrollView.Instance.Add ("defense:" + character.defense + " + " + item + " + " + buff + "\n");
	}
	public void ShowSpeed() {
		Character character = (Character)targetObject;
		int item = 0;
		for (int i=0; i<(int)character.items.Length; i++) {
			if(null != character.items[i]) {
				if(ItemInfo.Category.Weapon == character.items[i].info.category) {
					WeaponItemInfo info = (WeaponItemInfo)character.items[i].info;
					if(null != info) {
						item += info.speed;
					}
				}
				if(ItemInfo.Category.Armor == character.items[i].info.category) {
					ArmorItemInfo info = (ArmorItemInfo)character.items[i].info;
					if(null != info) {
						item += info.speed;
					}
				}

			}
		}
		int buff = 0;
		for (int i=0; i<character.buffs.Count; i++) {
			if (true == character.buffs[i].IsValid ()) {
				buff += character.buffs[i].ApplyBuff (character).speed;
			}
		}
		ScrollView.Instance.Add ("speed:" + character.speed + " + " + item + " + " + buff + "\n");
	}
	public void ShowBuffs() {
		Character character = (Character)targetObject;
		if (0 < character.buffs.Count) {
			ScrollView.Instance.Add ("buffs :\n");
			for(int i=0; i< character.buffs.Count; i++) {
				ScrollView.Instance.Add ("\t" + character.buffs[i].info.name + "\n");
			}
		}
	}
	public void ShowItems() {
		Character character = (Character)targetObject;

		for (int i=0; i<character.items.Length; i++) {
			if(null != character.items[i]) {
				ItemData data = character.items[i];
				ItemInfo info = data.info as ItemInfo;
				ScrollView.Instance.Add (((Character.EquipPart)i).ToString() + ":" + info.name + "\n", () => {
					//ItemView view = new ItemView(data);
					//view.ShowInfo();
				});
			}
		}
	}

	public virtual void ShowItemInfo(ItemData item) {}
	public virtual void ShowCharacterInfo() {
		Character character = (Character)targetObject;
		ScrollView.Instance.AddTitle (character.name);
		ScrollView.Instance.Add ("hp:" + character.health.current + "/" + character.health.max + "\n");
		ShowAttack ();
		ShowDefense ();
		ShowSpeed ();
		ShowBuffs ();
		ShowItems ();
	}

	public void CreateItemStackView(ItemData item) {
		GameObject obj = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Prefab/Map/ItemStackView.prefab", typeof(GameObject)) ) as GameObject;
		ItemStackView itemStackView = obj.GetComponent<ItemStackView> ();
		ItemStack itemStack = new ItemStack ();
		itemStack.item = item;
		itemStack.position.x = targetObject.position.x;
		itemStack.position.y = targetObject.position.y;
		itemStackView.SetObject(itemStack, "I");
		itemStackView.transform.SetParent (MapView.Instance.contents, false);
	}

	public override void Update() {
		TileView tileView = (TileView)Game.Instance.map.GetTile(targetObject.position.x, targetObject.position.y).view;
		tileView.SetHide(targetObject.visible);
		base.Update ();
	}
}
