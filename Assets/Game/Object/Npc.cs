using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Character {
	public List<string> quests = new List<string>();
	public ObjectView view;
	public Npc() {
	}

	public override void Update()
	{
		if (true == IsVisible (GameManager.Instance.player.position)) {
			QuestData quest = QuestManager.Instance.Find("quest_001");
			if(null != quest)
			{
				if(true == quest.IsAvailable())
				{
					quest.Start();
				}
				if(true == quest.IsComplete())
				{
				}
			}
		}

	}
	public override void OnCreate () {
		view = ObjectView.Create<ObjectView> (this, "N", Color.green);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}
	public override void OnDamage(Character attacker, int damage) {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>은(는) " + damage + "의 피해를 입었습니다.");
	}
	public override void OnDestroy() {
		GameObject.Destroy (view.gameObject);
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color> 은(는) 죽었습니다.");
	}
}