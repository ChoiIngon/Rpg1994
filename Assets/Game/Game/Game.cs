﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using LitJson;

public class Game : SingletonObject<Game> {
	public Player player;
	public Map map;
	public GameTurn gameTurn;
	public int currentTurn {
		get { return gameTurn.GetTurn (); }
	}
	private int lastTurn = 0;
	// Use this for initialization
	public Game() {
		Init ();
	}
	void Init() {
//		GameObject obj = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath("Assets/Prefab/Map/ItemStackView.prefab", typeof(GameObject)) ) as GameObject;
//		ItemStackView view = obj.GetComponent<ItemStackView> ();
//		view.SetObject (new ItemStack (), "+");
		gameTurn = new GameTurn ();
		gameTurn.Init<TurnCounter> ();
		player = new Player ();
		player.name = "You";
		player.sight = 4;
		player.health.current = 20;
		player.health.max = 20;
		player.health.amount = 2;
		player.health.time = 3;
		player.attack = 10;
		player.speed = 10;
		player.defense = 10;
		player.inventory.gold = 0;
		player.inventory.maxWeight = 30;
		player.inventory.weight = 0;
		player.inventory.Put (ItemManager.Instance.CreateInstance("sword_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("sword_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("sword_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("helmet_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("helmet_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("helmet_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("heal_potion_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance ("glove_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance ("shoes_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("heal_potion_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("attack_potion_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("attack_potion_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("attack_potion_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("poison_potion_001"));
		player.visible = true;
		player.position.x = 10;
		player.position.y = 10;

		map = new Map ();
	
		QuestManager.Instance.Init ();

		ScrollView.Instance.Add ("=====================================\n");
		ScrollView.Instance.Add ("#               RPG 1994            #\n");
		ScrollView.Instance.Add ("=====================================\n");
		ScrollView.Instance.Add ("Welcome to <b><color=#FF0000>Text</color></b> <b><color=#00FF00>RPG</color></b> <b><color=#0000FF>World</color></b>\n");
		ScrollView.Instance.Add ("All of this world are consisted of only text. ");
		ScrollView.Instance.Add ("Draw all things in your head.\n");
		ScrollView.Instance.Add ("\n");
		ScrollView.Instance.Add ("Well...now we start..Good luck son.\n");
	}
	
	public void Update() {
		if (lastTurn >= currentTurn) {
			return ;
		}

		foreach (var v in MonsterManager.Instance.monsters) {
			MonsterData monster = v.Value;
			monster.Action();
		}
		player.Action ();
		Debug.Log ("turn : " + currentTurn);
		lastTurn = currentTurn;
	}
}
