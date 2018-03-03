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

		public class Door {
			public enum DirectionType {
				Vertical, Horizon
			};

			public Position position = new Position();
			public DirectionType direction = DirectionType.Vertical;
		}

		public Room(MapData mapData)
		{
			map_data = mapData;

			int x = Random.Range(1, map_data.info.width - Room.MAX_SIZE - 1);
			int w = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);
			int y = Random.Range(1, map_data.info.height - Room.MAX_SIZE - 1);
			int h = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);

			left = Mathf.Max (1, x);
			right = Mathf.Min(left + w, map_data.info.width - 1);
			top = Mathf.Max(1, y);
			bottom = Mathf.Min(top + h, map_data.info.height - 1);

			left = (0 == left % 2) ? left + 1 : left;
			right = (0 == right % 2) ? right - 1 : right;
			top = (0 == top % 2) ? top + 1 : top;
			bottom = (0 == bottom % 2) ? bottom - 1 : bottom;
		}

		public bool Digging(/*int left, int right, int top, int bottom*/)
		{
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
			Dictionary<int, List<Door>> doors = new Dictionary<int, List<Door>> ();
			for (int y=top; y<=bottom; y++) {
				Door door = new Door ();
				door.direction = Door.DirectionType.Horizon;
				{
					int otherGroupID = map_data.GetTileGroupID(left - 2, y);
					int groupID = map_data.GetTileGroupID (left, y);
					if (0 != otherGroupID && otherGroupID != groupID) {
						door.position = new Position (left - 1, y);
						if (false == doors.ContainsKey (otherGroupID)) {
							doors.Add (otherGroupID, new List<Door> ());
						}
						doors[otherGroupID].Add (door);
					}
				}
				{
					int otherGroupID = map_data.GetTileGroupID(right + 2, y);
					int groupID = map_data.GetTileGroupID (right, y);
					if (0 != otherGroupID && otherGroupID != group_id) {
						if (false == doors.ContainsKey (otherGroupID)) {
							doors.Add (otherGroupID, new List<Door> ());
						}
						door.position = new Position (right + 1, y);
						doors[otherGroupID].Add (door);
					}
				}
			}
			for (int x=left; x<=right; x++) {
				Door door = new Door ();
				door.direction = Door.DirectionType.Vertical;
				{
					int otherGroupID = map_data.GetTileGroupID(x, top - 2);
					int groupID = map_data.GetTileGroupID (x, top);
					if (0 != otherGroupID && otherGroupID != groupID) {
						door.position = new Position (x, top - 1);
						if (false == doors.ContainsKey (otherGroupID)) {
							doors.Add (otherGroupID, new List<Door> ());
						}
						doors[otherGroupID].Add (door);
					}
				}
				{
					int otherGroupID = map_data.GetTileGroupID(x, bottom+2);
					int groupID = map_data.GetTileGroupID (x, bottom);
					if (0 != otherGroupID && otherGroupID != group_id) {
						door.position = new Position (x, bottom + 1);
						if (false == doors.ContainsKey (otherGroupID)) {
							doors.Add (otherGroupID, new List<Door> ());
						}
						doors[otherGroupID].Add (door);
					}
				}
			}


			foreach (var itr in doors) {
				int doorCount = 1;
				if (10 > Random.Range (1, 100)) {
					doorCount = 2;
				}
				List<Door> doorList = itr.Value;
				for (int currDoorCount = 0; currDoorCount < doorCount && 0 < doorList.Count; currDoorCount++) {
					int index = Random.Range (0, doorList.Count);
					Door door = doorList [index];
					if (Door.DirectionType.Horizon == door.direction) {
						int srcGroupID = map_data.GetTileGroupID (door.position.x - 1, door.position.y);
						int destGroupID = map_data.GetTileGroupID (door.position.x + 1, door.position.y);
						for (int i = 0; i < map_data.info.width * map_data.info.height; i++) {
							Tile tile = map_data.tiles [i];
							if (destGroupID == tile.group_id) {
								tile.group_id = srcGroupID;
							}
						}
						{
							Tile tile = map_data.GetTile (door.position.x, door.position.y);
							tile.group_id = srcGroupID;
						}
					} else {
						int srcGroupID = map_data.GetTileGroupID (door.position.x, door.position.y - 1);
						int destGroupID = map_data.GetTileGroupID (door.position.x, door.position.y + 1);
						for (int i = 0; i < map_data.info.width * map_data.info.height; i++) {
							Tile tile = map_data.tiles [i];
							if (destGroupID == tile.group_id) {
								tile.group_id = srcGroupID;
							}
						}
						{
							Tile tile = map_data.GetTile (door.position.x, door.position.y);
							tile.group_id = srcGroupID;
						}
					}
					currDoorCount++;
					doorList.RemoveAt (index);
				}
			}
		}
	}

}