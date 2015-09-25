using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileView : ObjectView {
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
			text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
			Tile tile = (Tile)targetObject;
			if (0 < tile.dictObjects.Count) {
				base.SetVisible(false);
			} else {
				base.SetVisible(true);
			}
		}
		else {
			text.color = new Color(text.color.r, text.color.g, text.color.b, 0.5f);;
		}

	}
	public override void Update() {
		base.Update ();
	}
}
