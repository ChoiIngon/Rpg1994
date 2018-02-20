using UnityEngine;
using System.Collections;

public class GameManager : Util.UI.Singleton<GameManager> {
	public Npc testNpc;
	public Transform popupLayer;
	private int lastTurn = 0;
	public int currentTurn {
		get { return Util.Timer<Util.TurnCounter>.Instance.GetTime(); }
	}

	void Start() {
		ItemManager.Instance.Init ();
		MonsterManager.Instance.Init ();
		QuestManager.Instance.Init ();
		/*
		Map.Instance.Init ();
		Player.Instance.name = "You";
		Player.Instance.sight = 6;
		Player.Instance.level = 1;
		
		Player.Instance.health.value = 150;
		Player.Instance.health.max = 150;
		Player.Instance.health.interval = 10;
		Player.Instance.health.recovery = 3;
		Player.Instance.health.time = 0;

		Player.Instance.stamina.value = 300;
		Player.Instance.stamina.max = 300;
		Player.Instance.stamina.interval = 0;
		Player.Instance.stamina.recovery = 0;
		Player.Instance.stamina.time = 0;
		
		Player.Instance.attack = 10;
		Player.Instance.speed = 10;
		Player.Instance.defense = 10;
		Player.Instance.inventory.gold = 0;
		Player.Instance.inventory.maxWeight = 30;
		Player.Instance.inventory.weight = 0;
		
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_002"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_003"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shield_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shield_002"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shield_003"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shirt_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shirt_002"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("shirt_003"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("ring_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("ring_002"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("ring_003"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("potion_heal_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("potion_stamina_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("potion_attack_001"));
		Player.Instance.inventory.Put (ItemManager.Instance.CreateInstance("potion_poison_001"));
		*/
		Init ();
	}

	public void Init () {

		//Map.Instance.Load ("Map/dungeon_001");
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			OnEscape();
		}
		if (lastTurn >= currentTurn) {
			return ;
		}

		//Map.Instance.Update ();
	

		lastTurn = currentTurn;
	}

	void OnEscape()
	{
		GameObject obj = null;
		if(true == PopupMessageView.Instance.gameObject.activeSelf)
		{	PopupMessageView.Instance.gameObject.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/PopupLayer/DropItemView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/PopupLayer/ItemInfoView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/PopupLayer/PlayerInfoView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}

		PopupMessageView.Instance.SetText ("Exit?", TextAnchor.MiddleCenter);
		PopupMessageView.Instance.SetWidth (400);
		PopupMessageView.Instance.AddSubmitListener (() => {
			Application.Quit();
		});
		PopupMessageView.Instance.gameObject.SetActive (true);
	}
}
