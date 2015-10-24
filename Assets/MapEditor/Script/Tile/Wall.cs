using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor {
	public class Wall : TileImpl {
		const int NONE = 0x0000;
		const int NORTH = 0x0001;
		const int SOUTH = 0x0010;
		const int EAST = 0x0100;
		const int WEST = 0x1000;
		public static Dictionary<int, string> PRESET = null;

		static Wall() {
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

		public static TileImpl Create() {
			return new Wall ();
		}

		public Wall() {
		}
		public override string GetType() {
			return "Wall";
		}
		public override void SetText (Tile tile) {
			int x = tile.position.x;
			int y = tile.position.y;
			int neighborDirection = NONE;
			Tile neighborTile = null;
			
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
				throw new System.Exception("can't put the wall at " + tile.position.ToString());
			}
			neighborTile = Map.Instance.GetTile (x + 1, y - 1);
			if ((neighborDirection & (EAST|NORTH)) == (EAST|NORTH) && null != neighborTile && "Wall" == neighborTile.type) {
				throw new System.Exception("can't put the wall at " + tile.position.ToString());
			}
			neighborTile = Map.Instance.GetTile (x + 1, y + 1);
			if ((neighborDirection & (EAST|SOUTH)) == (EAST|SOUTH) && null != neighborTile && "Wall" == neighborTile.type) {
				throw new System.Exception("can't put the wall at " + tile.position.ToString());
			}
			neighborTile = Map.Instance.GetTile (x - 1, y + 1);
			if ((neighborDirection & (WEST|SOUTH)) == (WEST|SOUTH) && null != neighborTile && "Wall" == neighborTile.type) {
				throw new System.Exception("can't put the wall at " + tile.position.ToString());
			}

			if (tile.text != PRESET [neighborDirection]) {
				tile.text = PRESET [neighborDirection];
				if(0 != (WEST&neighborDirection)) {
					Map.Instance.GetTile (x-1, y).SetText();
				}
				if(0 != (EAST&neighborDirection)) {
					Map.Instance.GetTile(x+1, y).SetText();
				}
				if(0 != (NORTH&neighborDirection)) {
					Map.Instance.GetTile(x, y-1).SetText();
				}
				if(0 != (SOUTH&neighborDirection)) {
					Map.Instance.GetTile(x, y+1).SetText();
				}
			}
		}
		public override void EditDialog(){
		}
	}
}