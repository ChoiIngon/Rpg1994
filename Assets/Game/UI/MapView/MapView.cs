using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MapView : SingletonBehaviour<MapView> {
	public TileView tileViewPref;
	public MonsterView monsterViewPref;
	public PlayerView playerViewPref;
	public TileView[] tileViews;

	public const int TILE_SIZE = 30;
	public const int MAP_WIDTH = 11;
	public const int MAP_HEIGHT = 9;
	public float MAP_SCALE = 1.0f;
	// Use this for initialization
	public void Load() {

		Game.Instance.map.Load ("Map/map_001");

		Transform contents = transform.FindChild ("Contents");
		if (null == contents) {
			throw new System.Exception ("can't find contents object");
		}
		while (0 < contents.childCount) {
			Transform child = contents.GetChild(0);
			Destroy(child.gameObject);
		}

		tileViews = new TileView [Game.Instance.map.width * Game.Instance.map.height];
		for(int y=0; y<Game.Instance.map.height; y++) {
			for(int x=0; x<Game.Instance.map.width; x++) {
				Tile tile = Game.Instance.map.GetTile(x, y);
				TileView tileView = Instantiate<TileView> (tileViewPref);

				tileView.transform.SetParent (contents.transform, false);
				tileView.SetTile(tile);
				tileViews[x+y*Game.Instance.map.width] = tileView;
			}
		}
		
		PlayerView playerView = Instantiate<PlayerView> (playerViewPref);
		Game.Instance.player.visible = true;
		playerView.transform.SetParent (contents.transform, false);
		playerView.SetObject(Game.Instance.player);
		
		foreach (var v in MonsterManager.Instance.monsters)
		{
			MonsterData monster = v.Value;
			/*
			GameObject monsterView = new GameObject();
			monsterView.AddComponent<MonsterView>();
			monsterView.GetComponent<MonsterView>().SetObject(monster);
			monsterView.transform.SetParent(contents.transform, false);
			*/
			MonsterView monsterView = Instantiate<MonsterView>(monsterViewPref);
			monsterView.transform.SetParent (contents.transform, false);
			monsterView.SetObject(monster);
			/*
			ObjectView objectView = Instantiate<ObjectView> (objectPref);
			objectView.transform.SetParent (contents.transform, false);
			objectView.SetObject(monster, "<b><color=red>M</color></b>");
			*/

		}

	}

	public void Start () {
		Load ();
	}

	public TileView GetTileView(int x, int y) {
		return tileViews [x + y * Game.Instance.map.width];
	}

	public void SetActivate(int x, int y, bool value) {
		TileView tileView = GetTileView (x, y);
		if (true == value) {
			tileView.SetVisible(1.0f);
		} else {
			tileView.SetVisible(0.0f);
		}
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
	
		Transform contents = transform.FindChild ("Contents");
		if (null == contents) {
			throw new System.Exception ("can't find contents object");
		}

		contents.localPosition = new Vector3 (viewX*TILE_SIZE, -viewY*TILE_SIZE);

		for(int i=0; i<contents.childCount; i++)
		{
			TileView tileView = contents.GetChild(i).GetComponent<TileView>();
			if(null != tileView)
			{
				Text text = tileView.GetTileText();
				if(true == tileView.tile.visible) {
					text.color = Color.white;
				}
				else {
					text.color = Color.gray;
				}
			}

			ObjectView objectView = contents.GetChild(i).GetComponent<ObjectView>();
			if(null != objectView)
			{
				objectView.gameObject.SetActive(objectView.targetObject.visible);
				if (true == objectView.targetObject.visible) {
					SetActivate (objectView.targetObject.position.x, objectView.targetObject.position.y, false);
				}
			}
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
}
