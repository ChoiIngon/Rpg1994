using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

namespace Map
{
	public class MapData {
		public MapInfo info = null;
		public int group_id = 0;
		public Tile[] tiles;
		public List<Room> rooms;

		public MapData(MapInfo info) {
			this.info = info;
			group_id = 1;
			tiles = new Tile[info.width * info.height];
			for (int i = 0; i < info.width * info.height; i++) {
				Tile tile = new Tile ();
				tile.position = new Position(i % info.width, i / info.width);
				tiles[i] = tile;
			}
		}
			
		public Tile GetTile(int x, int y) 
		{
			if (info == null) 
			{
				throw new System.Exception ("map is not initialized");
			}
			if (0 > x || info.width <= x || 0 > y || info.height <= y) 
			{
				return null;
			}
			return tiles [x + y * info.width];
		}

		public int GetTileGroupID(int x, int y)	{
			Tile tile = GetTile (x, y);
			if (null == tile) {
				return 0;
			}
			return tile.group_id;
		}

		public void FieldOfView(Position src, int range)
		{
			BresenhamCircle2D circle = new BresenhamCircle2D(src, range);
			foreach (Position circumference in circle)
			{
				BresenhamLine2D line = new BresenhamLine2D(src, circumference);
				foreach (Position position in line)
				{
					Tile tile = GetTile(position.x, position.y);
					tile.visit = true;
					tile.visible = true;

					if (0 < tile.objects.Count)                          
					{
						break;
					}
				}
			}
		}

		public List<Position> Raycast(Position s, Position e)
		{
			List<Position> positions = new List<Position> ();

			if (s == e) 
			{
				return positions;
			}

			int dx = Math.Abs(e.x - s.x);
			int dy = Math.Abs(e.y - s.y);

			if(dy <= dx)
			{
				int p = 2 * (dy - dx);
				int y = s.y;

				int inc_x = 1;
				if(e.x < s.x) 
				{
					inc_x = -1;
				}
				int inc_y = 1;
				if(e.y < s.y) {
					inc_y = -1;
				}
				for(int x=s.x; (e.x > s.x ? x <= e.x : x>=e.x) ; x += inc_x) {
					if(0 >= p) {
						p += 2 * dy;
					}
					else {
						p += 2 * (dy - dx);
						y += inc_y;
					}
					Tile tile = GetTile (x, y);
					if (0 < tile.objects.Count) {
						return positions;
					}
					positions.Add(tile.position);
				}
			}
			else {
				int p = 2 * (dx - dy);
				int x = s.x;

				int inc_x = 1;
				if(e.x < s.x) {
					inc_x = -1;
				}
				int inc_y = 1;
				if(e.y < s.y) {
					inc_y = -1;
				}
				for(int y=s.y; (e.y > s.y ? y <= e.y : y>=e.y) ; y += inc_y) {
					if(0 >= p) {
						p += 2 * dx;
					}
					else {
						p += 2 * (dx - dy);
						x += inc_x;
					}
					Tile tile = GetTile (x, y);
					if (0 < tile.objects.Count) {
						return positions;
					}
					positions.Add(tile.position);
				}
			}
			return positions;
		}
	}

}
