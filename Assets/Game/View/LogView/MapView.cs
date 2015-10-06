using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapView : MonoBehaviour {
	public const int TILE_SIZE = 30;
	public const int VIEW_WIDTH = 19;
	public const int VIEW_HEIGHT = 13;
	public int viewX = 0;
	public int viewY = 0;
	// Use this for initialization
	void Start () {
		LayoutElement layout = GetComponent<LayoutElement> ();
		layout.preferredWidth = TILE_SIZE * VIEW_WIDTH;
		layout.preferredHeight = TILE_SIZE * VIEW_HEIGHT;

		Object.Position position = Game.Instance.player.position;

		viewX = position.x - VIEW_WIDTH / 2;
		if (0 > viewX) { // if player locates more left side than center of map view 
			viewX = 0;
		}
		else if (position.x + VIEW_WIDTH/2 >= Game.Instance.map.width) { // if player locates right side than center of map view
			viewX = Game.Instance.map.width - VIEW_WIDTH;
		}

		viewY = position.y - VIEW_HEIGHT / 2;
		if (0 > viewY) { // if player locates more left side than center of map view 
			viewY = 0;
		}
		else if (position.y + VIEW_HEIGHT/2 >= Game.Instance.map.height) { // if player locates right side than center of map view
			viewY = Game.Instance.map.height - VIEW_HEIGHT;
		}


		for (int y=viewY; y<viewY+VIEW_HEIGHT; y++) {
			for (int x=viewX; x< viewX+VIEW_WIDTH; x++) {
				Tile tile = Game.Instance.map.GetTile(x, y);

				TileView tileView = null;
				if("" == tile.id) {
					tileView = CreateObjectView<TileView>(tile, ".", tile.color);
				}
				else {
					tileView = CreateObjectView<TileView>(tile, tile.id, tile.color);
				}

				if(true == tile.visible) {
					foreach(var v in tile.objects) {
						Object obj = v.Value;
						switch (obj.category) {
						case Object.Category.Monster : {
							MonsterData monster = (MonsterData)obj;
							CreateObjectView<ObjectView>(monster, "M", Color.red);
							if(MonsterData.State.Idle == monster.state) {
								LogView.Button("<color=red>" + monster.name + "[" + monster.position.x + "," + monster.position.y + "]</color>", () => {
									InfoView.MonsterInfo(monster);
								});
								LogView.Text ("이(가) 보입니다.\n");
							}
							break;
						}

						case Object.Category.Item : {	
							CreateObjectView<ObjectView>(v.Value, "$", Color.yellow);
							break;
						}
						}
					}
				}
			}
		}
		CreateObjectView<ObjectView> (Game.Instance.player, "@", Color.green);
	}

	public T CreateObjectView<T>(Object obj, string text, Color color) where T : ObjectView {
		GameObject objectView = GameObject.Instantiate(Resources.Load("Prefab/Map/ObjectView", typeof(GameObject)) ) as GameObject;
		objectView.AddComponent<T> ();
		objectView.transform.SetParent (transform, false);
		T tView = objectView.GetComponent<T> ();
		tView.name = text;
		tView.display.text = text;
		tView.display.color = color;
		tView.SetPosition (obj.position);
		tView.SetVisible (obj.visible);
		tView.transform.localPosition = new Vector3((tView.position.x-viewX) * MapView.TILE_SIZE, -(tView.position.y-viewY) * MapView.TILE_SIZE, 0);
		return tView;
	}
}
