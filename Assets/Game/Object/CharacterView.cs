﻿using UnityEngine;
using System.Collections;

public class CharacterView : ObjectView {
	public virtual void OnCreate(Character character) {}
	public virtual void OnMove(Character.DirectionType direction) {}
	public virtual void OnAttack(Character attacker, int damage) {
		//Character attacker = (Character)targetObject;
		/*
		if (0 < damage) {
			LogView.Button(attacker.name, () => { 
				ShowCharacterInfo();
			});
			
			LogView.Text (" damaged " + damage + " to ");
			LogView.Button(defender.name + "\n", () => { 
				((CharacterView)defender.view).ShowCharacterInfo();
			});
		} else {
			LogView.Button (defender.name, () => { 
				((CharacterView)defender.view).ShowCharacterInfo();
			});
			LogView.Text (" dodge!!\n");
		}
		*/
	}
	public virtual void OnDamage(Character attacker, Character defender, int damage) {}
	public virtual void OnDestroy()
	{
		//MapView.Instance.CreateView<ItemStackView> (itemStack, "$", Color.yellow);
	}

	public virtual void OnAttachBuff(BuffData buff) {}
	public virtual void OnDetachBuff(BuffData buff) {
		/*
		Character character = (Character)targetObject;
		LogView.Instance.Add (character.name, () => {
			((CharacterView)character.view).ShowCharacterInfo();
		});
		LogView.Instance.Add ("'s " + buff.info.name + " is expired\n");
		*/
	}
	/*
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
		LogView.Instance.Add ("attack:" + character.attack + " + " + item + " + " + buff + "\n");
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
		LogView.Instance.Add ("defense:" + character.defense + " + " + item + " + " + buff + "\n");
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
		LogView.Instance.Add ("speed:" + character.speed + " + " + item + " + " + buff + "\n");
	}
	public void ShowBuffs() {
		Character character = (Character)targetObject;
		if (0 < character.buffs.Count) {
			LogView.Instance.Add ("buffs :\n");
			for(int i=0; i< character.buffs.Count; i++) {
				LogView.Text ("\t" + character.buffs[i].info.name + "\n");
			}
		}
	}
	public void ShowItems() {
		Character character = (Character)targetObject;

		for (int i=0; i<character.items.Length; i++) {
			if(null != character.items[i]) {
				ItemData data = character.items[i];
				ItemInfo info = data.info as ItemInfo;
				Character.EquipPart part = (Character.EquipPart)i;
				LogView.Button (part.ToString() + ":" + info.name + "\n", () => {
					ShowEquipItemInfo (part, data);
				});
			}
		}
	}

	public virtual void ShowEquipItemInfo(Character.EquipPart part, ItemData item) {}
	public virtual void ShowCharacterInfo() {
		Character character = (Character)targetObject;
		LogView.Instance.AddTitle (character.name);
		LogView.Instance.Add ("hp:" + character.health.current + "/" + character.health.max + "\n");
		ShowAttack ();
		ShowDefense ();
		ShowSpeed ();
		ShowBuffs ();
		ShowItems ();
	}
	*/
}
