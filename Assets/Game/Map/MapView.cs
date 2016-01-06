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

	public void Init(Map map)
	{
		tiles = transform.FindChild ("Tiles");
		while(0<tiles.childCount) {
			Transform child = tiles.GetChild(0);
			child.SetParent(null);
			GameObject.Destroy(child.gameObject);
		}
		{
			RectTransform rect = GetComponent<RectTransform>();
			VIEW_WIDTH = (int)rect.rect.width/TILE_SIZE;
			VIEW_HEIGHT = (int)rect.rect.height/TILE_SIZE;
		}
		{
			RectTransform rect = tiles.GetComponent<RectTransform> ();
			rect.sizeDelta = new Vector2 (map.width * TILE_SIZE, map.height * TILE_SIZE);
		}
	}

	public void Center()
	{
		int x = GameManager.Instance.player.position.x - VIEW_WIDTH / 2;
		int y = GameManager.Instance.player.position.y - VIEW_HEIGHT / 2;
		tiles.localPosition = new Vector3 (-x * TILE_SIZE, y * TILE_SIZE, 0);
		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			Tile tile = GameManager.Instance.map.GetTile(view.position.x, view.position.y);
			if(null == tile) {
				throw new System.Exception("out of map position");
			}
			view.SetVisible(tile.visible);
		}
	}
}