using UnityEngine;
using System.Collections;

public class GameManager : Util.UI.Singleton<GameManager> {
	public Player player;
	public Npc testNpc;
	public Map map;

	private int lastTurn = 0;
	public int currentTurn {
		get { return Util.Timer<Util.TurnCounter>.Instance.GetTime(); }
	}

	void Start () {
		ItemManager.Instance.Init ();
		MonsterManager.Instance.Init ();
		QuestManager.Instance.Init ();

		map = new Map ();
		map.Load ("Map/dungeon_001");
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_001";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (25, 5);
			map.AddMonsterRegenSpot (spot);
		}
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_002";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (6, 17);
			map.AddMonsterRegenSpot (spot);
		}
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_001";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (20, 15);
			map.AddMonsterRegenSpot (spot);
		}
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_002";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (24, 5);
			map.AddMonsterRegenSpot (spot);
		}
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_001";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (5, 6);
			map.AddMonsterRegenSpot (spot);
		}
		{
			MonsterRegenSpot spot = new MonsterRegenSpot ();
			spot.id = "monster_001";
			spot.count = 0;
			spot.deadTime = 0;
			spot.interval = 5;
			spot.position = new Object.Position (11, 10);
			map.AddMonsterRegenSpot (spot);
		}

		testNpc = new Npc ();
		testNpc.id = "npc_001";
		testNpc.name = "npc a";
		testNpc.visible = false;
		testNpc.quests.Add ("quest_001");
		testNpc.SetPosition (new Object.Position (12, 11));

		player = new Player ();
		player.name = "You";
		player.sight = 6;

		player.health.value = 150;
		player.health.max = 150;
		player.health.interval = 10;
		player.health.recovery = 3;
		player.health.time = 0;

		player.stamina.value = 300;
		player.stamina.max = 300;
		player.stamina.interval = 0;
		player.stamina.recovery = 0;
		player.stamina.time = 0;

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
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_stamina_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_attack_001"));
		player.inventory.Put (ItemManager.Instance.CreateInstance("potion_poison_001"));
		
		player.visible = true;
		player.SetPosition (new Object.Position (5, 5));
		player.FieldOfView ();

		MapView.Instance.Init ();
		if (null != player.onCreate) {
			player.onCreate ();
		}
		if (null != testNpc.onCreate) {
			testNpc.onCreate ();
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			OnEscape();
		}
		if (lastTurn >= currentTurn) {
			return ;
		}

		MonsterManager.Instance.Update ();
		player.Update ();
		testNpc.Update ();
		map.Update ();
	
		lastTurn = currentTurn;
	}

	void OnEscape()
	{
		GameObject obj = null;
		obj = GameObject.Find ("Canvas/Popup/ExitPopup");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/Popup/DropItemView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/Popup/ItemView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/Popup/PlayerInfoView");
		if (null != obj && true == obj.activeSelf) {
			obj.SetActive (false);
			return;
		}
		obj = GameObject.Find ("Canvas/Popup/ExitPopup");
		obj.SetActive (true);
	}
}
