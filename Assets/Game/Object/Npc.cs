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
		view.SetVisible (visible);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public bool CheckQuest()
	{
		foreach(string questID in quests)
		{
			QuestData quest = QuestManager.Instance.Find(questID);
			if(null != quest)
			{
				if(true == quest.IsAvailable())
				{
					view.GetComponent<RectTransform>().SetSiblingIndex(MapView.Instance.tiles.childCount); 
					GameObject dialogView = GameObject.Instantiate(Resources.Load("Prefab/Dialog", typeof(GameObject)) ) as GameObject;
					dialogView.transform.SetParent(view.transform, false);

					string script = "";
					script += this.name + " :\n\n";
					foreach(QuestData.Dialouge dialouge in quest.startDialouges)
					{
						script += dialouge.script;
					}

					Dialog dialog = dialogView.GetComponent<Dialog> ();
					dialog.AddSubmitListener(() => {
						quest.Start();
						GameObject.Destroy(dialog.gameObject);
					});
					dialog.SetText(script);
					dialog.SetWidth(500);
					return true;
				}
			}
		}
		return false;
	}
}