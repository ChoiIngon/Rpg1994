using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Character {
	public List<string> quests = new List<string>();
	public ObjectView view;
	public string id;
	public List<string> dialouge = new List<string> ();
	public Npc(int x, int y) {
		category = Character.Category.NPC;
		position = new Object.Position (x, y);
		sight = 1;
		OnCreate ();
	}
	
	public override void OnCreate () {
		view = ObjectView.Create<ObjectView> (this, "N", Color.green);
		view.position = position;
		view.transform.SetParent (Map.Instance.view.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnTrigger(Object obj)
	{
	}

	public override void OnDestroy()
	{
		view.OnDestroy ();
	}
}