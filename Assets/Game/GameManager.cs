using UnityEngine;
using System.Collections;

public class GameManager : Util.UI.Singleton<GameManager> {
	public Player player;
	public Map map;

	private int lastTurn = 0;
	public int currentTurn {
		get { return Util.Timer<Util.TurnCounter>.Instance.GetTime(); }
	}

	void Start () {
		ItemManager.Instance.Init ();
		MonsterManager.Instance.Init ();
		QuestManager.Instance.Init ();

		player = new Player ();
		player.name = "You";
		player.sight = 6;
		player.health.value = 20;
		player.health.max = 20;
		player.health.interval = 2;
		player.health.recovery = 1;
		player.health.time = 0;
		player.attack = 10;
		player.speed = 10;
		player.defense = 10;
		player.inventory.gold = 0;
		player.inventory.maxWeight = 30;
		player.inventory.weight = 0;
		
		player.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("weapon_sword_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shield_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shield_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shield_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shirt_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shirt_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("shirt_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("ring_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("ring_002"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("ring_003"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_heal_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_heal_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_attack_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_attack_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_attack_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_poison_001"));
		
		player.visible = true;
		player.position.x = 10;
		player.position.y = 10;

		map = new Map ();
		map.Load ("Map/dungeon_001");

		MonsterRegenSpot spot = new MonsterRegenSpot ();
		spot.id = "monster_001";
		spot.count = 0;
		spot.deadTime = 0;
		spot.interval = 5;
		spot.position = new Object.Position (11, 10);
		map.AddMonsterRegenSpot (spot);

		MapView.Instance.Init ();

		LogView.Title ("RPG 1994");
		LogView.Text ("Welcome to <b><color=#FF0000>Text</color></b> <b><color=#00FF00>RPG</color></b> <b><color=#0000FF>World</color></b>\n\n");
		LogView.Text ("All of this world are consisted of only text. ");
		LogView.Text ("Draw all things in your head.\n");
		LogView.Text ("\n");
		LogView.Text ("Well...now we start..Good luck son.\n");
		LogView.Title (GameManager.Instance.map.name);
		LogView.Text (GameManager.Instance.map.description + "\n");

		player.OnCreate ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lastTurn >= currentTurn) {
			return ;
		}

		MonsterManager.Instance.Update ();
		player.Update ();
		map.Update ();
	
		lastTurn = currentTurn;
	}
}
