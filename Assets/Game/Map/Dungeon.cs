using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MapImpl {
	public int[] tiles;
	public int group;
	public int roomCount;
	public List<Room> rooms;
	public Object.Position enter;
	public Object.Position exit;

	public Dungeon(int width, int height, int roomCount)
	{
		this.group = 1;
		this.roomCount = roomCount;
		this.width = (0 == width % 2 ? width + 1 : width);
		this.height = (0 == height % 2 ? height + 1 : height);
		this.rooms = new List<Room> ();
		this.tiles = new int[this.width * this.height];
		for (int i = 0; i < tiles.Length; i++)
		{
			tiles[i] = 0;
		}
	}
	
	public int GetTileGroupID(int x, int y)
	{
		if (0 > x || x >= width || 0 > y || y >= height)
		{
			return 0;
		}
		return tiles[x + y * width];
	}

	public class Maze
	{
		public enum DirectionType
		{
			North, East, South, West, Max
		}
		public int group;
		public Dungeon dungeon;

		public Maze(Dungeon dungeon)
		{
			this.dungeon = dungeon;
			this.group = dungeon.group++;
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
			dungeon.tiles[x + y * dungeon.width] = group;
			
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
			//if (0 == Random.Range (0, 3)) {
				direction = directions [Random.Range (0, directions.Count)];
			//}

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
        public int left = 0;
        public int right = 0;
        public int top = 0;
        public int bottom = 0;
        public int width { get { return right - left; } }
        public int height { get { return bottom - top; } }
        public int area { get { return width * height; } }
		public int group = 0;
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

		public Object.Position GetRandomPosition()
		{
			return new Object.Position(Random.Range (left, right), Random.Range (top, bottom));
		}

		public void Digging()
		{
			for(int y=this.top; y<=this.bottom; y++)
			{
				for (int x = this.left; x <= this.right; x++)
				{
					if(0 != dungeon.tiles[x + y * dungeon.width])
					{
						return;
					}
				}
			}
			group = dungeon.group++;
			for(int y=this.top; y<=this.bottom; y++)
			{
				for (int x = this.left; x <= this.right; x++)
				{
					dungeon.tiles[x + y * dungeon.width] = group;
				}
			}
			dungeon.rooms.Add (this);
		}

		public void Connect() {
			List<Connector> connectors = new List<Connector> ();
			for (int y=top; y<=bottom; y++) {
				int other = 0;
				other = dungeon.GetTileGroupID(left-2, y);
				if(0 != other && group != other)
				{
					Connector connector = new Connector();
					connector.x = left - 1;
					connector.y = y;
					connector.direction = Connector.DirectionType.Horizon;
					connectors.Add (connector);
				}
				other = dungeon.GetTileGroupID(right+2, y);
				if(0 != other && group != other)
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
				if(0 != other && group != other)
				{
					Connector connector = new Connector();
					connector.x = x;
					connector.y = top-1;
					connector.direction = Connector.DirectionType.Vertical;
					connectors.Add (connector);
				}
				other = dungeon.GetTileGroupID(x, bottom-2);
				if(0 != other && group != other)
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
							dungeon.tiles[i] = src;
							count++;
						}
					}
					if(0 < count || 0 == Random.Range(0, 30)) {
						dungeon.tiles[connector.x + connector.y * dungeon.width] = src;
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
							dungeon.tiles[i] = src;
							count++;
						}
					}
					if(0 < count || 0 == Random.Range(0, 30)) {
						dungeon.tiles[connector.x + connector.y * dungeon.width] = src;
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
	public enum TileType {
		Unused,
		Wall,
		Door,
		Floor,
		Corridor
	}

    public override void Generate()
    {
		for(int i=0; i<1000 && rooms.Count < this.roomCount; i++)
		{
			int x = Random.Range(1, this.width - 10);
			int w = Random.Range(4, 9);
			int y = Random.Range(1, this.height - 10);
			int h = Random.Range(4, 9);
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

		bool delete;
		do {
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
							tiles[x + y * width] = 0;
							delete = true;
						}
					}
				}
			}
		} while(delete);
	
		for (int y=0; y<height; y++) {
			for (int x=0; x<width; x++) {
				if(0 == this.tiles[x + y * this.width])
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
						wall.SetPosition(new Object.Position(x, y));
					}
				}
			}
		}

		enter = rooms [0].GetRandomPosition ();
    }
}
