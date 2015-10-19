using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor {
	public class Tile : MonoBehaviour {
		public static char [] block = new char[]{'─', '│', '┌', '┐', '┘', '└', '├', '┤', '┬', '┴', '┼'};
		public string type;
		public Object.Position position;
		public Text tile;

		public void Init() {
			Button button = transform.GetComponent<Button> ();
			Transform trans = button.transform.FindChild ("Text");
			if (null == trans) {
				throw new System.Exception ("can't find Text object");
			}
			tile = trans.GetComponent<Text>();
			position = new Object.Position (0, 0);
		}

		public void OnClick()
		{
			if (null == TileSelector.selected) {
				return;
			}

			if (Input.GetMouseButtonDown (0)) {
				TileSelector selector = TileSelector.selected;

				if ("Wall" == selector.type) {
					SetWall ();
					type = selector.type;
					tile.color = selector.color;
				} else if ("Erase" == selector.type) {
					type = "";
					tile.text = "";
				} else {
					type = selector.type;
					tile.text = selector.text;
					tile.color = selector.color;
				}
			} else if (Input.GetMouseButtonDown (1)) {
				if("" == type)
				{
					Debug.Log ("right click");
				}
			}
		}

		public void SetWall() {
			const int NONE = 0x0000;
			const int NORTH = 0x0001;
			const int SOUTH = 0x0010;
			const int EAST = 0x0100;
			const int WEST = 0x1000;

			Dictionary<int, string> PRESET = new Dictionary<int, string>();

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
				
			int x = position.x;
			int y = position.y;
			int neighborDirection = NONE;
			Tile neighborTile = null;
			/*
			neighborTile = Map.Instance.GetTile (x - 1, y - 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x + 1, y - 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x + 1, y - 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x - 1, y + 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			*/
			neighborTile = Map.Instance.GetTile (x - 1, y);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				neighborDirection |= WEST;
			}
			neighborTile = Map.Instance.GetTile (x + 1, y);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				neighborDirection |= EAST;
			}
			neighborTile = Map.Instance.GetTile (x, y - 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				neighborDirection |= NORTH;
			}
			neighborTile = Map.Instance.GetTile (x, y + 1);
			if (null != neighborTile && "Wall" == neighborTile.type) {
				neighborDirection |= SOUTH;
			}

			neighborTile = Map.Instance.GetTile (x - 1, y - 1);
			if ((neighborDirection & (WEST|NORTH)) == (WEST|NORTH) && null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x + 1, y - 1);
			if ((neighborDirection & (EAST|NORTH)) == (EAST|NORTH) && null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x + 1, y + 1);
			if ((neighborDirection & (EAST|SOUTH)) == (EAST|SOUTH) && null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
			neighborTile = Map.Instance.GetTile (x - 1, y + 1);
			if ((neighborDirection & (WEST|SOUTH)) == (WEST|SOUTH) && null != neighborTile && "Wall" == neighborTile.type) {
				return;
			}
		
			if (tile.text != PRESET [neighborDirection]) {
				type = "Wall";
				tile.text = PRESET [neighborDirection];
				if(0 != (WEST&neighborDirection)) {
					Map.Instance.GetTile (x-1, y).SetWall();
				}
				if(0 != (EAST&neighborDirection)) {
					Map.Instance.GetTile(x+1, y).SetWall();
				}
				if(0 != (NORTH&neighborDirection)) {
					Map.Instance.GetTile(x, y-1).SetWall();
				}
				if(0 != (SOUTH&neighborDirection)) {
					Map.Instance.GetTile(x, y+1).SetWall();
				}
			}
		}
	}
}