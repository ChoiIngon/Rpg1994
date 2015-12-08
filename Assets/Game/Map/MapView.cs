using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapView : Util.UI.Singleton<MapView> {
	public const int TILE_SIZE = 30;
	public const int MAP_WIDTH = 22;
	public const int MAP_HEIGHT = 22;
	[HideInInspector]
	public Transform tiles;
	[HideInInspector]
	public Transform objects;
	public void Init()
	{
		tiles = transform.FindChild ("Tiles");
		objects = transform.FindChild ("Objects");

		RectTransform rect = tiles.GetComponent<RectTransform> ();
		rect.sizeDelta = new Vector2 (GameManager.Instance.map.width * TILE_SIZE, GameManager.Instance.map.height * TILE_SIZE);

		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			GameObject.Destroy(view);
		}

		for (int i=0; i<GameManager.Instance.map.tiles.Length; i++) {
			global::Tile tile = GameManager.Instance.map.tiles[i];
			
			TileView view = null;
			if("" == tile.id) {
				view = CreateTileView(tile, ".", tile.color);
			}
			else {
				view = CreateTileView(tile, tile.id, tile.color);
			}
			view.SetVisible(false);
		}

		for (int i=0; i<objects.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			GameObject.Destroy(view);
		}

		Center();
	}

	public void Center()
	{
		int x = GameManager.Instance.player.position.x - MAP_WIDTH / 2;
		if (0 > x) { // if player locates more left side than center of map view 
			x = 0;
		}
		else if (GameManager.Instance.player.position.x + MAP_WIDTH/2 >= GameManager.Instance.map.width) { // if player locates right side than center of map view
			x = GameManager.Instance.map.width - MAP_WIDTH;
		}
		
		int y = GameManager.Instance.player.position.y - MAP_HEIGHT / 2;
		if (0 > y) { // if player locates more left side than center of map view 
			y = 0;
		}
		else if (GameManager.Instance.player.position.y + MAP_HEIGHT/2 >= GameManager.Instance.map.height) { // if player locates right side than center of map view
			y = GameManager.Instance.map.height - MAP_HEIGHT;
		}

		tiles.localPosition = new Vector3 (-x * TILE_SIZE, y * TILE_SIZE, 0);
		objects.localPosition = tiles.localPosition;

		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			global::Tile tile = GameManager.Instance.map.GetTile(view.position.x, view.position.y);
			view.SetVisible(tile.visible);
		}
	}

	private TileView CreateTileView(Object obj, string text, Color color) {
		TileView view = ObjectView.Create<TileView> (obj, text, color);
		view.transform.SetParent (tiles, false);
		view.transform.localPosition = new Vector3(view.position.x * TILE_SIZE, -view.position.y * TILE_SIZE, 0);
		return view;
	}
	public void AddItemStack(ItemStack stack)
	{
		ObjectView view = ObjectView.Create<ObjectView>(stack.position, "$", Color.yellow);
		view.transform.SetParent (objects, false);
		view.transform.localPosition = new Vector3(view.position.x * TILE_SIZE, -view.position.y * TILE_SIZE, 0);
	}
}