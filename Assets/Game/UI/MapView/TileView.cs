using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileView : ObjectView {
	public void SetTile(Tile tile)
	{
		string text = ".";
		if ("" != tile.id) {
			text = tile.id;
		}
		base.SetObject(tile, text);
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
	public void SetHide(bool value) {
		Transform tileTextTransform = transform.FindChild ("Text");
		if (null == tileTextTransform) {
			throw new System.Exception ("can't find text object");
		}
		Text text = tileTextTransform.GetComponent<Text> ();
		if (true == value) {
			text.color = new Color (text.color.r, text.color.g, text.color.b, 0.0f);
		} else {
			text.color = new Color (text.color.r, text.color.g, text.color.b, 1.0f);
		}
	}
	public override void Update() {
		Text text = GetTileText();
		
		if(true == targetObject.visible) {
			text.color = Color.white;
		}
		else {
			text.color = Color.gray;
		}
		base.Update ();
	}
}
