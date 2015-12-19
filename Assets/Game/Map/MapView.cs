using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapView : Util.UI.Singleton<MapView> {
	public const int TILE_SIZE = 45;
	[HideInInspector]
	public int VIEW_WIDTH = 0;
	[HideInInspector]
	public int VIEW_HEIGHT = 0;
	[HideInInspector]
	public Transform tiles;
	public void Init()
	{
		tiles = transform.FindChild ("Tiles");
		{
			RectTransform rect = GetComponent<RectTransform>();
			VIEW_WIDTH = (int)rect.rect.width/TILE_SIZE;
			VIEW_HEIGHT = (int)rect.rect.height/TILE_SIZE;
		}
		{
			RectTransform rect = tiles.GetComponent<RectTransform> ();
			rect.sizeDelta = new Vector2 (GameManager.Instance.map.width * TILE_SIZE, GameManager.Instance.map.height * TILE_SIZE);
		}
		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			GameObject.Destroy(view.gameObject);
		}

		for (int i=0; i<GameManager.Instance.map.tiles.Length; i++) {
			Tile tile = GameManager.Instance.map.tiles[i];
			TileView view = CreateTileView(tile, tile.color);
			view.SetVisible(false);
		}

		for (int i=0; i<tiles.childCount; i++) {
			Tile tile = GameManager.Instance.map.tiles[i];
			if(null == tile.FindObject<Wall>())
			{
				continue;
			}
			TileView view = tiles.GetChild(i).GetComponent<TileView>();
			view.Init(tile);
		}

		Center();
	}

	public void Center()
	{
		int x = GameManager.Instance.player.position.x - VIEW_WIDTH / 2;
		int y = GameManager.Instance.player.position.y - VIEW_HEIGHT / 2;
		tiles.localPosition = new Vector3 (-x * TILE_SIZE, y * TILE_SIZE, 0);
		for (int i=0; i<tiles.childCount; i++) {
			ObjectView view = tiles.GetChild(i).GetComponent<ObjectView>();
			global::Tile tile = GameManager.Instance.map.GetTile(view.position.x, view.position.y);
			if(null == tile) {
				throw new System.Exception("out of map position");
			}
			view.SetVisible(tile.visible);
		}
	}

	private TileView CreateTileView(Tile obj, Color color) {
		TileView view = ObjectView.Create<TileView> (obj, ".", color);
		view.transform.SetParent (tiles, false);
		view.transform.localPosition = new Vector3(view.position.x * TILE_SIZE, -view.position.y * TILE_SIZE, 0);
		return view;
	}

	public ObjectView AddItemStack(ItemStack stack)
	{
		ObjectView view = ObjectView.Create<ObjectView>(stack, "$", Color.yellow);
		view.transform.SetParent (tiles, false);
		view.transform.localPosition = new Vector3(view.position.x * TILE_SIZE, -view.position.y * TILE_SIZE, 0);
		return view;
	}
}