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
		onCreate += OnCreate;
	}
	
	public void OnCreate () {
		view = ObjectView.Create<ObjectView> (this, "N", Color.green);
		view.SetVisible (visible);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}
}