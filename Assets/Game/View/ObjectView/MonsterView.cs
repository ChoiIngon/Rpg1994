using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterView : CharacterView {
	public override void Destroy() {
		base.Destroy ();
		MonsterData monster = (MonsterData)targetObject;
		LogView.Instance.Add (monster.name + " died\n");
	}

	public override void ShowCharacterInfo() {
		MonsterData monster = (MonsterData)targetObject;
		LogView.Instance.AddTitle (monster.name);
		LogView.Instance.Add (monster.info.description + "\n");
		LogView.Instance.Add ("\n");
		LogView.Instance.Add ("hp:" + monster.health.current + "/" + monster.health.max + "\n");
		ShowAttack ();
		ShowDefense ();
		ShowSpeed ();
		ShowBuffs ();
		ShowItems ();
	}

}
