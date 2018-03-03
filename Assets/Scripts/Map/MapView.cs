using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map {
	public class MapView : MonoBehaviour {
		[System.Serializable]
		public class SpriteInfo {
			public string name;
			public Sprite sprite;
		}
		private Dictionary<string, Sprite> _spriteInfos;
		public Sprite GetSprite(string name) {
			if(false == _spriteInfos.ContainsKey(name))
			{
				throw new System.Exception("can not find sprite(name:" + name + ")");
			}
			return _spriteInfos[name];
		}
		public MapData map_data = null;

		public SpriteInfo[] spriteInfos;

		public List<SpriteRenderer> spriteRenderers;
		// Use this for initialization
		void Start () {
			MapInfo info = new MapInfo ();
			info.width = 40;
			info.height = 40;
			info.room_count = 10;

			map_data = info.CreateInstance ();

			_spriteInfos = new Dictionary<string, Sprite> ();
			foreach (SpriteInfo spriteInfo in spriteInfos) {
				_spriteInfos.Add (spriteInfo.name, spriteInfo.sprite);
			}

			spriteRenderers = new List<SpriteRenderer> ();

			for (int y = 0; y < map_data.info.width; y++) {
				for (int x = 0; x < map_data.info.height; x++) {
					Tile tile = map_data.GetTile (x, y);
					GameObject go = new GameObject ();
					go.name = "tile_" + x + "_" + y;

					SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer> ();
					if (0 == tile.group_id) {
						spriteRenderer.sprite = GetSprite ("empty");
					}
					else {
						spriteRenderer.sprite = GetSprite ("tile_" + tile.group_id %10);
					}

					spriteRenderer.transform.position = new Vector3 (x * 0.32f, y * 0.32f, 0.0f);
					spriteRenderer.transform.SetParent (this.transform, false);
					spriteRenderers.Add(spriteRenderer);
				}
			}
		}
	}
}