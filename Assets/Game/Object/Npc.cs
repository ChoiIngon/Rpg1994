using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Character {
	public List<string> quests = new List<string>();
	public ObjectView view;
	public string id;
	public List<string> dialouge = new List<string> ();
	public Npc() {
		category = Character.Category.NPC;
		sight = 1;
	}
	
	public override void OnCreate () {
		view = ObjectView.Create<ObjectView> (this, "N", Color.green);
	}

	public override void SetPosition(Object.Position position) {
		base.SetPosition (position);
		view.SetPosition (position);
	}

	public override void OnTrigger(Object obj)
	{
	}

	public override void OnDestroy()
	{
		view.OnDestroy ();
	}
}