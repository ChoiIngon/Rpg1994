using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileView : MonoBehaviour {
	public Tile tile;

	public void SetTile(Tile tile)
	{
		Transform tileTextTransform = transform.FindChild ("Text");
		if (null == tileTextTransform) {
			throw new System.Exception ("can't find text object");
		}
		Text text = tileTextTransform.GetComponent<Text> ();
		this.tile = tile;
		if ("" != this.tile.id) {
			text.text = this.tile.id;
		}
		transform.localPosition = new Vector3(this.tile.position.x * MapView.TILE_SIZE, -this.tile.position.y * MapView.TILE_SIZE, 0);
	}
	public void SetTileText(string t) {
		Transform tileTextTransform = transform.FindChild ("Text");
		if (null == tileTextTransform) {
			throw new System.Exception ("can't find text object");
		}
		Text text = tileTextTransform.GetComponent<Text> ();
		text.text = t;
	}
	public Text GetTileText() {
		Transform tileTextTransform = transform.FindChild ("Text");
		if (null == tileTextTransform) {
			throw new System.Exception ("can't find text object");
		}
		return tileTextTransform.GetComponent<Text> ();
	}
	public void SetVisible(float a) {
		Transform tileTextTransform = transform.FindChild ("Text");
		if (null == tileTextTransform) {
			throw new System.Exception ("can't find text object");
		}
		Text text = tileTextTransform.GetComponent<Text> ();
		text.color = new Color(text.color.r, text.color.g, text.color.b, a);
	}
}
