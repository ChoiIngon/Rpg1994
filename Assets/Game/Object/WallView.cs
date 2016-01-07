using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WallView : ObjectView {
	static int NONE = 0x0000;
	static int NORTH = 0x0001;
	static int SOUTH = 0x0010;
	static int EAST = 0x0100;
	static int WEST = 0x1000;
	public static Dictionary<int, string> PRESET = null;
	
	static WallView() {
		PRESET = new Dictionary<int, string>();
		PRESET [NONE] = "─";
		PRESET [NORTH] = "│";
		PRESET [SOUTH] = "│";
		PRESET [EAST] = "─";
		PRESET [WEST] = "─";
		PRESET [NORTH|SOUTH] = "│";
		PRESET [NORTH|EAST] = "└";
		PRESET [NORTH|WEST] = "┘";
		PRESET [SOUTH|EAST] = "┌";
		PRESET [SOUTH|WEST] = "┐";
		PRESET [EAST|WEST] = "─";
		PRESET [NORTH|SOUTH|EAST] = "├";
		PRESET [NORTH|SOUTH|WEST] = "┤";
		PRESET [SOUTH|EAST|WEST] = "┬";
		PRESET [EAST|WEST|NORTH] = "┴";
		PRESET [NORTH|SOUTH|EAST|WEST] = "┼";
	}

	public void Init (Wall wall) {
		/*
		int x = wall.position.x;
		int y = wall.position.y;
		int neighborDirection = NONE;
		Tile neighborTile = null;
		
		neighborTile = GameManager.Instance.map.GetTile (x - 1, y);
		if (null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			neighborDirection |= WEST;
		}
		neighborTile = GameManager.Instance.map.GetTile (x + 1, y);
		if (null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			neighborDirection |= EAST;
		}
		neighborTile = GameManager.Instance.map.GetTile (x, y - 1);
		if (null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			neighborDirection |= NORTH;
		}
		neighborTile = GameManager.Instance.map.GetTile (x, y + 1);
		if (null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			neighborDirection |= SOUTH;
		}
		/*
		neighborTile = GameManager.Instance.map.GetTile (x - 1, y - 1);
		if ((neighborDirection & (WEST|NORTH)) == (WEST|NORTH) && null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			throw new System.Exception("can't put the wall at " + wall.position.ToString());
		}
		neighborTile = GameManager.Instance.map.GetTile (x + 1, y - 1);
		if ((neighborDirection & (EAST|NORTH)) == (EAST|NORTH) && null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			throw new System.Exception("can't put the wall at " + wall.position.ToString());
		}
		neighborTile = GameManager.Instance.map.GetTile (x + 1, y + 1);
		if ((neighborDirection & (EAST|SOUTH)) == (EAST|SOUTH) && null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			throw new System.Exception("can't put the wall at " + wall.position.ToString());
		}
		neighborTile = GameManager.Instance.map.GetTile (x - 1, y + 1);
		if ((neighborDirection & (WEST|SOUTH)) == (WEST|SOUTH) && null != neighborTile && null != neighborTile.FindObject<Wall> ()) {
			throw new System.Exception("can't put the wall at " + wall.position.ToString());
		}

		
		if (display.text != PRESET [neighborDirection]) {
			display.text = PRESET [neighborDirection];
			if(0 != (WEST&neighborDirection)) {
				Tile t = GameManager.Instance.map.GetTile (x-1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			if(0 != (EAST&neighborDirection)) {
				Tile t = GameManager.Instance.map.GetTile (x+1, y);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			if(0 != (NORTH&neighborDirection)) {
				Tile t = GameManager.Instance.map.GetTile (x, y-1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
			if(0 != (SOUTH&neighborDirection)) {
				Tile t = GameManager.Instance.map.GetTile (x, y+1);
				Wall w = t.FindObject<Wall>();
				if(null != w) {
					w.view.Init (w);
				}
			}
		}
		*/
        //display.text = "#";
	}

	public override void SetVisible (bool value)
	{
		if(true == value) {
			gameObject.SetActive(true);
			display.color = new Color(display.color.r, display.color.g, display.color.b, 1.0f);
		}
		else {
			Tile tile = GameManager.Instance.map.GetTile (position.x, position.y);
			if(null == tile) {
				throw new System.Exception("out of map position");
			}
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
}
