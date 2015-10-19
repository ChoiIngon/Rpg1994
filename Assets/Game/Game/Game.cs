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
	}
	public void Init() {
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
		map.Load ("Map/dungeon_001");
		QuestManager.Instance.Init ();

		for(int i=0; i<15; i++) {
			{
				MonsterData monster = MonsterManager.Instance.CreateInstance("monster_001");
				monster.position.x = UnityEngine.Random.Range(0, Game.Instance.map.width-1);
				monster.position.y = UnityEngine.Random.Range(0, Game.Instance.map.height-1);
			}
			{
				MonsterData monster = MonsterManager.Instance.CreateInstance("monster_002");
				monster.position.x = UnityEngine.Random.Range(0, Game.Instance.map.width-1);
				monster.position.y = UnityEngine.Random.Range(0, Game.Instance.map.height-1);
			}
		}
	}
	
	public void Update() {
		if (lastTurn >= currentTurn) {
			return ;
		}

		foreach (var v in MonsterManager.Instance.monsters) {
			MonsterData monster = v.Value;
			monster.Update();
		}
		player.Update ();
		lastTurn = currentTurn;
	}
}
