using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterView : CharacterView {

	// Use this for initialization
	public void SetObject(Object o) {
		base.SetObject (o, "M");
	}
	
	public void Destroy() {
		base.Destroy ();
		MonsterData monster = (MonsterData)targetObject;
		ScrollView.Instance.Add (monster.name, () => { 
			((MonsterView)monster.view).ShowCharacterInfo();
		});
		ScrollView.Instance.Add (" die\n");
	}

	public override void ShowCharacterInfo() {
		MonsterData monster = (MonsterData)targetObject;
		ScrollView.Instance.AddTitle (monster.name);
		ScrollView.Instance.Add (monster.info.description + "\n");
		ScrollView.Instance.Add ("\n");
		ScrollView.Instance.Add ("hp:" + monster.health.current + "/" + monster.health.max + "\n");
		ShowAttack ();
		ShowDefense ();
		ShowSpeed ();
		ShowBuffs ();
		ShowItems ();
	}
}
