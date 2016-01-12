using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Dungeon : MapImpl {
	private int[] tiles_;
	private int groupID;
	private int roomCount;
	private List<Room> rooms;
	private List<MonsterSpawnSpot> spwanSpots;
	private JSONNode root;

	public Dungeon(JSONNode root){
		this.root = root;
		this.groupID = 1;
		Room.MIN_SIZE = root ["room"] ["min"].AsInt;
		Room.MAX_SIZE = root ["room"] ["max"].AsInt;
		int w = root ["size"] ["width"].AsInt;
		int h = root ["size"] ["height"].AsInt;
		this.width = (0 == w % 2 ? w + 1 : w);
		this.height = (0 == h % 2 ? h + 1 : h);
		this.roomCount = root ["room"] ["count"].AsInt;
		this.rooms = new List<Room> ();
		this.tiles_ = new int[this.width * this.height];
		for (int i = 0; i < tiles_.Length; i++)
		{
			tiles_[i] = 0;
		}
		this.tiles = new Tile[this.width * this.height];
	}

	public override void Generate()
	{
		GenerateTiles ();
		GenerateMonsters ();
		GenerateGateways ();
		GenerateNPC ();
	}

	private void GenerateNPC()
	{
		JSONNode json = root ["npc"];
		for (int i=0; i<json.Count; i++) {
			Npc npc = new Npc();
			npc.id = json[i]["id"];
			npc.SetPosition(rooms[json[i]["room"].AsInt].GetRandomPosition());
		}
	}

	private void GenerateTiles()
	{
		for (int i = 0; i< this.tiles.Length; i++) {
			Tile tile = new Tile();
			tile.SetPosition(new Position(i % this.width, i / this.width));
			tiles[i] = tile;
		}

		for(int i=0; i<1000 && rooms.Count <= this.roomCount; i++)
		{
			int x = Random.Range(1, this.width - Room.MAX_SIZE - 1);
			int w = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);
			int y = Random.Range(1, this.height - Room.MAX_SIZE - 1);
			int h = Random.Range(Room.MIN_SIZE, Room.MAX_SIZE);
			Room room = new Room (this, x, x + w, y, y + h);
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

	public void GenerateMonsters()
	{
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
	}

	public void GenerateGateways() {
		JSONNode gateways = root ["gateway"];
		for (int i=0; i<gateways.Count; i++) {
			Gateway gateway = new Gateway();
			gateway.dest.id = gateways[i]["id"];
			gateway.dest.name = gateways[i]["name"];
			gateway.dest.map = gateways[i]["map"];
			gateway.SetPosition(rooms[gateways[i]["room"].AsInt].GetRandomPosition());
		}
	}

	public override void Update()
	{
		foreach (MonsterSpawnSpot spot in spwanSpots)
		{
			spot.Update();
		}
	}

	public int GetTileGroupID(int x, int y)
	{
		if (0 > x || x >= width || 0 > y || y >= height)
		{
			return 0;
		}
		return tiles_[x + y * width];
	}

	public class Maze
	{
		public enum DirectionType
		{
			North, East, South, West, Max
		}
		public int groupID;
		public Dungeon dungeon;

		public Maze(Dungeon dungeon)
		{
			this.dungeon = dungeon;
			this.groupID = dungeon.groupID++;
		}
		
		public void Digging(DirectionType direction, int x, int y)
		{
			if (1 > x || x >= dungeon.width-1 || 1 > y || y >= dungeon.height-1)
			{
				return;
			}

			int count = 0;
			for (int dy = -1; dy <= 1; dy += 2)
			{
				if (0 == dungeon.GetTileGroupID(x, y + dy))
				{
					count++;
				}
			}
			for (int dx = -1; dx <= 1; dx += 2)
			{
				if (0 == dungeon.GetTileGroupID(x + dx, y))
				{
					count++;
				}   
			}
			
			if(3 > count)
			{
				return;
			}
			dungeon.tiles_[x + y * dungeon.width] = groupID;
			
			List<DirectionType> directions = new List<DirectionType>();

			if (1 == y % 2) {
				directions.Add (DirectionType.East);
				directions.Add (DirectionType.West);
			}
			if (1 == x % 2) {
				directions.Add (DirectionType.North);
				directions.Add (DirectionType.South);
			}

			directions.Add (direction);
			if (0 == Random.Range (0, 2)) {
				direction = directions [Random.Range (0, directions.Count)];
			}

			while (0 < directions.Count)
			{
				directions.Remove(direction);
				switch (direction)
				{
				case DirectionType.North:
					Digging(direction, x, y - 1);
					break;
				case DirectionType.East:
					Digging(direction, x + 1, y);
					break;
				case DirectionType.South:
					Digging(direction, x, y + 1);
					break;
				case DirectionType.West:
					Digging(direction, x - 1, y);
					break;
				}
				if (0 == directions.Count)
				{
					return;
				}
				direction = directions[Random.Range (0, directions.Count)];
			}
		}
	}
    public class Room
    {
		public Dungeon dungeon;
		public static int MIN_SIZE;
		public static int MAX_SIZE;
        public int left = 0;
        public int right = 0;
        public int top = 0;
        public int bottom = 0;
        public int width { get { return right - left; } }
        public int height { get { return bottom - top; } }
        public int area { get { return width * height; } }
		public int groupID = 0;
		public Room(Dungeon dungeon, int left, int right, int top, int bottom)
		{
			this.dungeon = dungeon;
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;

			if (1 > this.left) {
				this.left = 1;
			}
			
			if (dungeon.width - 1 <= this.right) {
				this.right = dungeon.width - 1;
			}
			
			if (1 > this.top) {
				this.top = 1;
			}
			
			if (dungeon.height - 1 <= this.bottom) {
				this.bottom = dungeon.height - 1;
			}

			this.left = (0 == this.left % 2) ? this.left + 1 : this.left;
			this.right = (0 == this.right % 2) ? this.right - 1 : this.right;
			this.top = (0 == this.top % 2) ? this.top + 1 : this.top;
			this.bottom = (0 == this.bottom % 2) ? this.bottom - 1 : this.bottom;
		}

		public Position GetRandomPosition()
		{
			return new Position(Random.Range (left, right), Random.Range (top, bottom));
		}

		public void Digging()
		{
			for(int y=this.top; y<=this.bottom; y++)
			{
				for (int x = this.left; x <= this.right; x++)
				{
					if(0 != dungeon.tiles_[x + y * dungeon.width])
					{
						return;
					}
				}
			}
			groupID = dungeon.groupID++;
			for(int y=this.top; y<=this.bottom; y++)
			{
				for (int x = this.left; x <= this.right; x++)
				{
					dungeon.tiles_[x + y * dungeon.width] = groupID;
				}
			}
			dungeon.rooms.Add (this);
		}

		public void Connect() {
			List<Connector> connectors = new List<Connector> ();
			for (int y=top; y<=bottom; y++) {
				int other = 0;
				other = dungeon.GetTileGroupID(left-2, y);
				if(0 != other && groupID != other)
				{
					Connector connector = new Connector();
					connector.x = left - 1;
					connector.y = y;
					connector.direction = Connector.DirectionType.Horizon;
					connectors.Add (connector);
				}
				other = dungeon.GetTileGroupID(right+2, y);
				if(0 != other && groupID != other)
				{
					Connector connector = new Connector();
					connector.x = right + 1;
					connector.y = y;
					connector.direction = Connector.DirectionType.Horizon;
					connectors.Add (connector);
				}
			}
			for (int x=left; x<=right; x++) {
				int other = 0;
				other = dungeon.GetTileGroupID(x, top-2);
				if(0 != other && groupID != other)
				{
					Connector connector = new Connector();
					connector.x = x;
					connector.y = top-1;
					connector.direction = Connector.DirectionType.Vertical;
					connectors.Add (connector);
				}
				other = dungeon.GetTileGroupID(x, bottom-2);
				if(0 != other && groupID != other)
				{
					Connector connector = new Connector();
					connector.x = x;
					connector.y = bottom-1;
					connector.direction = Connector.DirectionType.Vertical;
					connectors.Add (connector);
				}
			}

			while (0 < connectors.Count) {
				int index = Random.Range(0, connectors.Count);
				Connector connector = connectors[index];
				if(Connector.DirectionType.Horizon == connector.direction)
				{
					int src = dungeon.GetTileGroupID(connector.x-1, connector.y);
					int dest = dungeon.GetTileGroupID(connector.x+1, connector.y);
					int count = 0;
					for(int i=0;i<dungeon.width*dungeon.height; i++)
					{
						int x = i%dungeon.width;
						int y = i/dungeon.width;
						if(src != dest && dest == dungeon.GetTileGroupID(x, y))
						{
							dungeon.tiles_[i] = src;
							count++;
						}
					}
					if(0 < count || 0 == Random.Range(0, 30)) {
						dungeon.tiles_[connector.x + connector.y * dungeon.width] = src;
					}
				}
				else
				{
					int src = dungeon.GetTileGroupID(connector.x, connector.y-1);
					int dest = dungeon.GetTileGroupID(connector.x, connector.y+1);
					int count = 0;
					for(int i=0;i<dungeon.width*dungeon.height; i++)
					{
						int x = i%dungeon.width;
						int y = i/dungeon.width;
						if(src != dest && dest == dungeon.GetTileGroupID(x, y))
						{
							dungeon.tiles_[i] = src;
							count++;
						}
					}
					if(0 < count || 0 == Random.Range(0, 30)) {
						dungeon.tiles_[connector.x + connector.y * dungeon.width] = src;
					}
				}
				connectors.RemoveAt(index);
			}
		}
    }

	public class Connector {
		public enum DirectionType {
			Vertical, Horizon
		};
		public int x, y;
		public DirectionType direction;
	}
}
