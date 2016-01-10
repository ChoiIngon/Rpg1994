using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapView : MonoBehaviour {
	public static int TILE_SIZE = 45;
	[HideInInspector]
	public int VIEW_WIDTH = 0;
	[HideInInspector]
	public int VIEW_HEIGHT = 0;
	[HideInInspector]
	public Transform tiles;

	void Start()
	{
		RectTransform rect = GetComponent<RectTransform>();
		VIEW_WIDTH = (int)rect.rect.width/TILE_SIZE;
		VIEW_HEIGHT = (int)rect.rect.height/TILE_SIZE;
		tiles = transform.FindChild ("Tiles");
	}

	public void Init()
	{
		while(0<tiles.childCount) {
			Transform child = tiles.GetChild(0);
			child.SetParent(null);
			GameObject.Destroy(child.gameObject);
		}
		RectTransform rect = tiles.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (Map.Instance.width * TILE_SIZE, Map.Instance.height * TILE_SIZE);

		/*
		foreach (Tile tile in Map.Instance.tiles) {
			tile.OnCreate();
			foreach(var v in tile.objects)
			{
				v.Value.OnCreate();
			}
		}
		*/
	}

	public void Center()
	{
		int x = Player.Instance.position.x - VIEW_WIDTH / 2;
		int y = Player.Instance.position.y - VIEW_HEIGHT / 2;
		tiles.localPosition = new Vector3 (-x * TILE_SIZE, y * TILE_SIZE, 0);
		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			Tile tile = Map.Instance.GetTile(view.position.x, view.position.y);
			if(null == tile) {
				throw new System.Exception("out of map position");
			}
			view.SetVisible(tile.visible);
		}
	}
}