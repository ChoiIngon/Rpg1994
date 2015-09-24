using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MapView : SingletonBehaviour<MapView> {
	public const int TILE_SIZE = 30;
	public const int MAP_WIDTH = 11;
	public const int MAP_HEIGHT = 9;
	public float MAP_SCALE = 1.0f;
	public Transform contents;

	public void Init(string path) {
		contents = transform.FindChild ("Contents");
		if (null == contents) {
			throw new System.Exception ("can't find contents object");
		}

		while (0 < contents.childCount) {
			Transform child = contents.GetChild(0);
			Destroy(child.gameObject);
		}


		Game.Instance.map.Load (path);
		foreach(Tile tile in Game.Instance.map.tiles) {
			if("" == tile.id) {
				CreateView<TileView>(tile, ".");
			}
			else {
				CreateView<TileView>(tile, tile.id);
			}
		}

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
		foreach (var v in MonsterManager.Instance.monsters)
		{
			MonsterData monster = v.Value;
			CreateView<MonsterView>(monster, "<color=red>M</color>");
		}

		CreateView<PlayerView> (Game.Instance.player, "<color=green>@</color>");
	}

	public void Start () {
		Init ("Map/map_001");
	}
	
	void Update() {
		Object.Position playerPosition = Game.Instance.player.position;
	
		int viewX = MAP_WIDTH * (int)MAP_SCALE / 2 - playerPosition.x;
		if (0 < viewX) {
			viewX = 0;
		}
		if (MAP_WIDTH * (int)MAP_SCALE - Game.Instance.map.width > viewX) {
			viewX = MAP_WIDTH * (int)MAP_SCALE - Game.Instance.map.width;
		}
		int viewY = MAP_HEIGHT * (int)MAP_SCALE / 2 - playerPosition.y;
		if (0 < viewY) {
			viewY = 0;
		}
		if (MAP_HEIGHT * (int)MAP_SCALE - Game.Instance.map.height > viewY) {
			viewY = MAP_HEIGHT * (int)MAP_SCALE - Game.Instance.map.height;
		}
		contents.localPosition = new Vector3 (viewX*TILE_SIZE, -viewY*TILE_SIZE);
		foreach (var v in MonsterManager.Instance.monsters)
		{
			MonsterData monster = v.Value;
			MonsterView monsterView = (MonsterView)monster.view;
			monsterView.gameObject.SetActive(monster.visible);
		}
	}

	public void SetMapScale(float scale) {
		RectTransform rt = GetComponent<RectTransform> ();
		MAP_SCALE = scale;

		if(1.0f < scale) {
			transform.localPosition = new Vector3 (-MAP_WIDTH * MAP_SCALE / 2 * TILE_SIZE, transform.localPosition.y);
		} else {
			rt.anchoredPosition = new Vector2 (-MAP_WIDTH * MAP_SCALE * TILE_SIZE, 0);
		}
		
		rt.sizeDelta = new Vector2 (MAP_WIDTH * MAP_SCALE * TILE_SIZE, MAP_HEIGHT * MAP_SCALE * TILE_SIZE);
	}

	public void OnClick() {
		RectTransform rt = GetComponent<RectTransform> ();
		if (1.0f == MAP_SCALE) {
			SetMapScale(2.0f);
		} else {
			SetMapScale(1.0f);
		}

		rt.sizeDelta = new Vector2 (MAP_WIDTH * MAP_SCALE * TILE_SIZE, MAP_HEIGHT * MAP_SCALE * TILE_SIZE);
	}

	public T CreateView<T>(Object o, string c) where T : ObjectView {
		GameObject objectView = GameObject.Instantiate(Resources.Load("Prefab/Map/ObjectView", typeof(GameObject)) ) as GameObject;
		objectView.AddComponent<T> ();
		objectView.transform.SetParent (contents.transform, false);
		T tView = objectView.GetComponent<T> ();
		tView.GetComponent<T>().SetObject(o, c);
		return tView;
	}
}
