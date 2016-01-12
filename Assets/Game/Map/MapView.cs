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
	[HideInInspector]
	private Rect viewRect;
	void Start()
	{
		viewRect = GetComponent<RectTransform>().rect;
		VIEW_WIDTH = (int)viewRect.width/TILE_SIZE;
		VIEW_HEIGHT = (int)viewRect.height/TILE_SIZE;
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

	void Update()
	{
		for(int i=0; i<tiles.childCount; i++) {
			Transform child = tiles.GetChild(i);
			if(0 <= child.localPosition.x + tiles.localPosition.x && viewRect.width > child.localPosition.x + tiles.localPosition.x &&
			   0 > child.localPosition.y + tiles.localPosition.y && viewRect.height > child.localPosition.y - tiles.localPosition.y
			)
			{
				child.gameObject.SetActive(true);
			}
			else {
				child.gameObject.SetActive(false);
			}
		}
	}
}