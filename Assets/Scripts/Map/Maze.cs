using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
	public class Maze
	{
		public enum DirectionType
		{
			North, East, South, West, Max
		}
		MapData map_data;

		public Maze(MapData mapData)
		{
			map_data = mapData;
		}

		public void Digging(int x, int y)
		{
			if (1 > x || x >= map_data.info.width-1 || 1 > y || y >= map_data.info.height-1)
			{
				Debug.Log ("fail to dig maze(x:" + x + ", y:" + y + ")");
				return;
			}

			int count = 0;
			for (int dy = -1; dy <= 1; dy += 2)
			{
				Tile tile = map_data.GetTile (x, y + dy);
				if (tile == null || 0 == tile.group_id) {
					count++;
				}
			}
			for (int dx = -1; dx <= 1; dx += 2)
			{
				Tile tile = map_data.GetTile (x + dx, y);
				if (tile == null || 0 == tile.group_id) {
					count++;
				}
			}

			if(3 > count)
			{
				Debug.Log ("dig maze count under 3(x:" + x + ", y:" + y + ")");
				return;
			}

			{
				Tile tile = map_data.GetTile (x, y);
				tile.group_id = map_data.group_id++;
			}

			List<DirectionType> directions = new List<DirectionType>();
			if (1 == y % 2) {
				directions.Add (DirectionType.East);
				directions.Add (DirectionType.West);
			}
			if (1 == x % 2) {
				directions.Add (DirectionType.North);
				directions.Add (DirectionType.South);
			}

			//directions.Add (direction);
			DirectionType direction = directions [Random.Range (0, directions.Count)];

			while (0 < directions.Count)
			{
				directions.Remove(direction);
				switch (direction)
				{
				case DirectionType.North:
					Digging(x, y - 1);
					break;
				case DirectionType.East:
					Digging(x + 1, y);
					break;
				case DirectionType.South:
					Digging(x, y + 1);
					break;
				case DirectionType.West:
					Digging(x - 1, y);
					break;
				}
				if (0 == directions.Count)
				{
					return;
				}
				direction = directions[Random.Range (0, directions.Count)];
			}
			return;
		}
	}
}