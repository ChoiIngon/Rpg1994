using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
	public class Room
	{
		public static int MIN_SIZE = 6;
		public static int MAX_SIZE = 6;
		public int left = 0;
		public int right = 0;
		public int top = 0;
		public int bottom = 0;
		public int width { get { return right - left; } }
		public int height { get { return bottom - top; } }
		public int area { get { return width * height; } }
		public int group_id = 0;
		public MapData map_data;

		public Room(MapData mapData)
		{
			map_data = mapData;
		}

		public bool Digging(int left, int right, int top, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;

			this.left = (0 == this.left % 2) ? this.left + 1 : this.left;
			this.right = (0 == this.right % 2) ? this.right - 1 : this.right;
			this.top = (0 == this.top % 2) ? this.top + 1 : this.top;
			this.bottom = (0 == this.bottom % 2) ? this.bottom - 1 : this.bottom;

			for(int y=this.top; y<=this.bottom; y++) {
				for (int x = this.left; x <= this.right; x++) {
					if(0 != map_data.GetTileGroupID (x, y))
					{
						return false;
					}
				}
			}

			for(int y=this.top; y<=this.bottom; y++) {
				for (int x = this.left; x <= this.right; x++) {
					Tile tile = map_data.GetTile (x, y);
					tile.group_id = map_data.group_id;
				}
			}
			map_data.group_id++;
			return true;
		}

		public Position GetRandomPosition()
		{
			return new Position(Random.Range (left, right), Random.Range (top, bottom));
		}
			
		public void Connect() {
			List<Door> doors = new List<Door> ();
			for (int y=top; y<=bottom; y++) {
				Door door = new Door ();
				door.direction = Door.DirectionType.Horizon;
				{
					int otherGroupID = map_data.GetTileGroupID(left - 2, y);
					if (0 != otherGroupID && otherGroupID != group_id) {
						door.position = new Position (left - 1, y);
						doors.Add (door);
					}
				}
				{
					int otherGroupID = map_data.GetTileGroupID(right + 2, y);
					if (0 != otherGroupID && otherGroupID != group_id) {
						door.position = new Position (right + 1, y);
						doors.Add (door);
					}
				}
			}
			for (int x=left; x<=right; x++) {
				Door door = new Door ();
				door.direction = Door.DirectionType.Vertical;
				{
					int otherGroupID = map_data.GetTileGroupID(x, top - 2);
					if (0 != otherGroupID && otherGroupID != group_id) {
						door.position = new Position (x, top - 1);
						doors.Add (door);
					}
				}
				{
					int otherGroupID = map_data.GetTileGroupID(x, bottom+2);
					if (0 != otherGroupID && otherGroupID != group_id) {
						door.position = new Position (x, bottom+1);
						doors.Add (door);
					}
				}
			}

			while (0 < doors.Count) {
				int index = Random.Range(0, doors.Count);
				Door door = doors[index];
				if(Door.DirectionType.Horizon == door.direction) {
					int srcGroupID = map_data.GetTileGroupID (door.position.x - 1, door.position.y);
					int destGroupID = map_data.GetTileGroupID(door.position.x + 1, door.position.y);
					for(int i=0;i<map_data.info.width*map_data.info.height; i++)
					{
						int x = i%map_data.info.width;
						int y = i/map_data.info.width;
						Tile tile = map_data.GetTile (x, y);
						if(srcGroupID != destGroupID && destGroupID == tile.group_id)
						{
							tile.group_id = srcGroupID;
						}
					}
				}
				else
				{
					int srcGroupID = map_data.GetTileGroupID(door.position.x, door.position.y-1);
					int destGroupID = map_data.GetTileGroupID(door.position.x, door.position.y+1);
					for(int i=0;i<map_data.info.width*map_data.info.height; i++)
					{
						int x = i%map_data.info.width;
						int y = i/map_data.info.width;
						Tile tile = map_data.GetTile (x, y);
						if(srcGroupID != destGroupID && destGroupID == tile.group_id)
						{
							tile.group_id = srcGroupID;
						}
					}
				}
				doors.RemoveAt(index);
			}
		}
	}

}