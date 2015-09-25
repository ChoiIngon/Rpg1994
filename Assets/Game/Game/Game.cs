using UnityEngine;
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

		LogView.Title ("RPG 1994");
		LogView.Text ("Welcome to <b><color=#FF0000>Text</color></b> <b><color=#00FF00>RPG</color></b> <b><color=#0000FF>World</color></b>\n\n");
		LogView.Text ("All of this world are consisted of only text. ");
		LogView.Text ("Draw all things in your head.\n");
		LogView.Text ("\n");
		LogView.Text ("Well...now we start..Good luck son.\n");
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
		lastTurn = currentTurn;
	}
}
