using UnityEngine;
using System.Collections;

public class CharacterView : ObjectView {
	public void OnAttack(Character defender, int damage) {
		Character attacker = (Character)targetObject;
		if (0 < damage) {
			ScrollView.Instance.Add (attacker.name, () => { 
				((CharacterView)attacker.view).ShowInfo();
			});
			
			ScrollView.Instance.Add (" damaged " + damage + " to ");
			ScrollView.Instance.Add (defender.name + "\n", () => { 
				((CharacterView)defender.view).ShowInfo();
			});
		} else {
			ScrollView.Instance.Add (defender.name, () => { 
				((CharacterView)attacker.view).ShowInfo();
			});
			ScrollView.Instance.Add (" dodge!!\n");
		}
	}
	private void ShowAttack() {
		Character character = (Character)targetObject;
		string item = "0";
		if (null != character.weapon) {
			WeaponItemInfo info = (WeaponItemInfo)character.weapon.info;
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
	private void ShowDefense() {
		Character character = (Character)targetObject;
		int item = 0;
		for (int i=0; i<(int)character.armor.Length; i++) {
			if(null != character.armor[i]) {
				ArmorItemInfo info = (ArmorItemInfo)character.armor[i].info;
				item += info.defense;
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
	private void ShowSpeed() {
		Character character = (Character)targetObject;
		int item = 0;
		if (null != character.weapon) {
			WeaponItemInfo info = (WeaponItemInfo)character.weapon.info;
			item += info.speed;
		}
		
		for (int i=0; i<(int)character.armor.Length; i++) {
			if(null != character.armor[i]) {
				ArmorItemInfo info = (ArmorItemInfo)character.armor[i].info;
				item += info.speed;
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
		if (null != character.weapon) {
			ScrollView.Instance.Add ("Weapon:" + character.weapon.info.name + "\n", () => {
				ItemView view = new ItemView(character.weapon);
				view.ShowInfo();
			});
		}
		
		for (int i=0; i<(int)character.armor.Length; i++) {
			if(null != character.armor[i]) {
				ArmorItemData data = character.armor[i];
				ArmorItemInfo info = data.info as ArmorItemInfo;
				ScrollView.Instance.Add (info.type.ToString() + ":" + info.name + "\n", () => {
					ItemView view = new ItemView(data);
					view.ShowInfo();
				});
			}
		}
	}
	public void ShowInfo() {
		Character character = (Character)targetObject;
		ScrollView.Instance.AddTitle (character.name);
		ScrollView.Instance.Add ("hp:" + character.health.current + "/" + character.health.max + "\n");
		ShowAttack ();
		ShowDefense ();
		ShowSpeed ();
		ShowBuffs ();
		ShowItems ();
	}
}
