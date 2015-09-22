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
			((MonsterView)monster.view).ShowInfo();
		});
		ScrollView.Instance.Add (" die\n");
	}
}
