//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class TileView : ObjectView {
	public override void SetVisible (bool value)
	{
		if(true == value) {
			gameObject.SetActive(true);
			display.color = new Color(display.color.r, display.color.g, display.color.b, 1.0f);
		}
		else {
			Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
			if(true == tile.visit)
			{
				gameObject.SetActive(true);
				display.color = new Color(display.color.r, display.color.g, display.color.b, 0.5f);
			}
			else{
				gameObject.SetActive(false);
			}
		}
	}
};

