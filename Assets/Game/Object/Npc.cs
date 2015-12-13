using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Character {
	public List<string> quests = new List<string>();
	public ObjectView view;

	public Npc() {
		sight = 1;
	}

	public override void Update()
	{
		if (sight >= Vector2.Distance (position, GameManager.Instance.player.position)) {
			foreach(string questID in quests)
			{
				QuestData quest = QuestManager.Instance.Find(questID);
				if(null != quest)
				{
					if(true == quest.IsAvailable())
					{
						quest.Start();
						return;
					}
				}
				if(QuestData.State.OnExecute == quest.state)
				{
					quest.IsComplete();
				}
			}
		}
	}
	
	public override void OnCreate () {
		view = ObjectView.Create<ObjectView> (this, "N", Color.green);
		view.SetVisible (visible);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}
	public override void OnDestroy() {
		GameObject.Destroy (view.gameObject);
	}
}