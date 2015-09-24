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

	public override void SetVisible (bool value)
	{
		Text text = GetTileText();
		if(true == targetObject.visible) {
			text.color = Color.white;
			Tile tile = (Tile)targetObject;
			if (0 < tile.dictObjects.Count) {
				base.SetVisible(false);
			} else {
				base.SetVisible(true);
			}
		}
		else {
			text.color = Color.gray;
		}

	}
	public override void Update() {

		base.Update ();
	}
}
