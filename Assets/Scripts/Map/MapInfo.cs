using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public class MapInfo {
		public string id;
		public string name;
		public string description;

		public int width = 20;
		public int height = 20;
		public int room_count = 4;

		public MapData CreateInstance()
		{
			width = (0 == this.width % 2 ? this.width + 1 : this.width);
			height = (0 == this.height % 2 ? this.height + 1 : this.height);

			MapData mapData = new MapData (this);
			List<Room> rooms = new List<Room> ();

			for(int i=0; i<1000 && rooms.Count < room_count; i++)
			{
				int x = Random.Range(1, this.width - Room.MAX_SIZE - 1);
				int w = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);
				int y = Random.Range(1, this.height - Room.MAX_SIZE - 1);
				int h = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);

				int left = Mathf.Max (1, x);
				int right = Mathf.Min(left + w, width - 1);
				int top = Mathf.Max(1, y);
				int bottom = Mathf.Min(top + h, height - 1);

				Room room = new Room (mapData);
				if (true == room.Digging (left, right, top, bottom)) {
					Debug.Log ("group_id:" + mapData.group_id + ", x:" + x + ", w:" + w + ", y:" + y + ", h:" + h +
						", left:" + left + ", right:" + right + ", top:" + top + ", bottom:" + bottom);
					rooms.Add (room);
				}
			}

			for (int y=1; y<height-1; y++) {
				for (int x=1; x<width-1; x++) {
					Tile tile = mapData.GetTile (x, y);
					if(1 == x % 2 && 1 == y % 2 && 0 == tile.group_id) {
						Maze maze = new Maze (mapData);
						maze.Digging (x, y);
						mapData.group_id++;
					}
				}
			}

			foreach (Room room in rooms) {
				room.Connect();
			}
			/*
			bool delete = true;
			while (delete) {
				delete = false;
				for (int y = 1; y < height - 1; y++) {
					for (int x = 1; x < width - 1; x++) {
						if (0 != mapData.GetTileGroupID (x, y)) {
							int count = 0;
							for (int dy = -1; dy <= 1; dy += 2) {
								if (0 == mapData.GetTileGroupID (x, y + dy)) {
									count++;
								}
							}
							for (int dx = -1; dx <= 1; dx += 2) {
								if (0 == mapData.GetTileGroupID (x + dx, y)) {
									count++;
								}   
							}

							if (3 <= count) {
								//tiles_ [x + y * width] = 0;
								delete = true;
							}
						}
					}
				}
			}
			*/
			return mapData;
		}




		public void Generate()
		{
			//GenerateTiles ();
			GenerateMonsters ();
			GenerateGateways ();
			GenerateNPC ();
		}

		private void GenerateNPC()
		{
			/*
			JSONNode json = root ["npc"];
			for (int i=0; i<json.Count; i++) {
				Npc npc = new Npc();
				npc.id = json[i]["id"];
				npc.SetPosition(rooms[json[i]["room"].AsInt].GetRandomPosition());
			}
			*/
		}
		/*
		private void GenerateTiles()
		{
			for(int i=0; i<1000 && mapData.rooms.Count <= room_count; i++)
			{
				int x = Random.Range(1, this.width - Room.MAX_SIZE - 1);
				int w = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);
				int y = Random.Range(1, this.height - Room.MAX_SIZE - 1);
				int h = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);

				//int left, int right, int top, int bottom)
				int left = x;
				if (1 > left) {
					left = 1;
				}
				int right = left + w;
				if (width - 1 <= right) {
					right = width - 1;
				}
				int top = y;
				if (1 > top) {
					top = 1;
				}
				int bottom = top + height;
				if (height - 1 <= bottom) {
					bottom = height - 1;
				}

				Room room = new Room (left, right, top, bottom);

				room.Digging ();
			}

			for (int y=1; y<height-1; y++) {
				for (int x=1; x<width-1; x++) {
					if(1 == x%2 && 1 == y%2 && 0 == GetTileGroupID(x, y)) {
						Maze maze = new Maze (this);
						maze.Digging (Maze.DirectionType.West, x, y);
					}
				}
			}

			foreach (Room room in rooms) {
				room.Connect();
			}

			bool delete = true;
			while(delete) {
				delete = false;
				for (int y=1; y<height-1; y++) {
					for (int x=1; x<width-1; x++) {
						if(0 != GetTileGroupID(x, y))
						{
							int count = 0;
							for (int dy = -1; dy <= 1; dy += 2)
							{
								if (0 == GetTileGroupID(x, y + dy))
								{
									count++;
								}
							}
							for (int dx = -1; dx <= 1; dx += 2)
							{
								if (0 == GetTileGroupID(x + dx, y))
								{
									count++;
								}   
							}

							if(3 <= count)
							{
								tiles_[x + y * width] = 0;
								delete = true;
							}
						}
					}
				}
			}

			for (int y=0; y<height; y++) {
				for (int x=0; x<width; x++) {
					if(0 == this.GetTileGroupID(x, y))
					{
						int count = 0;
						count += GetTileGroupID(x-1, y-1);
						count += GetTileGroupID(x, y-1);
						count += GetTileGroupID(x+1, y-1);
						count += GetTileGroupID(x+1, y);
						count += GetTileGroupID(x+1, y+1);
						count += GetTileGroupID(x, y+1);
						count += GetTileGroupID(x-1, y+1);
						count += GetTileGroupID(x-1, y);

						if(0 != count)
						{
							Wall wall = new Wall();
							wall.SetPosition(new Position(x, y));
						}
					}
				}
			}

			start = rooms [root ["start_room"].AsInt].GetRandomPosition ();
		}
		*/
		public void GenerateMonsters()
		{
			/*
			spwanSpots = new List<MonsterSpawnSpot> ();
			JSONNode json = root ["monster"];
			for (int i=0; i<json.Count; i++) {
				MonsterSpawnSpot spot = new MonsterSpawnSpot();
				spot.id = json[i]["id"];
				spot.count = json[i]["count"].AsInt;
				spot.interval = json[i]["interval"].AsInt;
				Util.RangeInt roomID = new Util.RangeInt(json[i]["room"]);
				spot.position = rooms[(int)roomID].GetRandomPosition();
				spwanSpots.Add (spot);
			}
			*/
		}

		public void GenerateGateways() {
			/*
			JSONNode gateways = root ["gateway"];
			for (int i=0; i<gateways.Count; i++) {
				Gateway gateway = new Gateway();
				gateway.dest.id = gateways[i]["id"];
				gateway.dest.name = gateways[i]["name"];
				gateway.dest.map = gateways[i]["map"];
				gateway.SetPosition(rooms[gateways[i]["room"].AsInt].GetRandomPosition());
			}
			*/
		}
	}
}