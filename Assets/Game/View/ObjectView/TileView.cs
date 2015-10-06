using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileView : ObjectView {
	public override void SetVisible (bool value)
	{
		if(true == value) {
			display.color = new Color(display.color.r, display.color.g, display.color.b, 1.0f);
			Tile tile = Game.Instance.map.GetTile(position.x, position.y);
			if(0 < tile.objects.Count) {
				gameObject.SetActive(false);
			}
		}
		else {
			display.color = new Color(display.color.r, display.color.g, display.color.b, 0.5f);
		}
	}
}
