﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFind_AStar {
	public Dictionary<int, Node> openNodes = new Dictionary<int, Node>();
	public Dictionary<int, Node> closeNodes = new Dictionary<int, Node>();
	public Object.Position destination;
	public class Node {
		public int id {
			get {
				return position.x + position.y * GameManager.Instance.map.width;
			}
		}
		public int cost {
			get {
				return path_cost + expect_cost;
			}
		}
		public int path_cost = 0;
		public int expect_cost = 0;
		public Object.Position position;
		public Node parent = null;
		private PathFind_AStar finder;

		public Node(PathFind_AStar finder, Node parent, Object.Position position, int path_cost)
		{
			this.finder = finder;
			this.position = position;
			this.expect_cost += Mathf.Abs(finder.destination.x - position.x);
			this.expect_cost += Mathf.Abs(finder.destination.y - position.y);
			this.path_cost = path_cost;
			this.parent = parent;
		}

		public Object.Position FindNextPath() {
			if (true == finder.openNodes.ContainsKey (id)) {
				finder.openNodes.Remove (id);
			}

			if(true == finder.closeNodes.ContainsKey(id) ){
				return null;
			}
			finder.closeNodes.Add (id, this);
			if(finder.destination == position)
			{
				Node trace = this;
				while(null != trace.parent)
				{
					trace = trace.parent;
				}
				return trace.position;
			}

			List<Object.Position> childPositions = new List<Object.Position> ();
			childPositions.Add(new Object.Position(position.x-1, position.y)); 
			childPositions.Add(new Object.Position(position.x+1, position.y)); 
			childPositions.Add(new Object.Position(position.x, position.y-1)); 
			childPositions.Add(new Object.Position(position.x, position.y+1)); 

			List<Node> children = new List<Node> ();
			foreach (Object.Position childPosition in childPositions) {
				if(0 > childPosition.x || 0 > childPosition.y || GameManager.Instance.map.width <= childPosition.x || GameManager.Instance.map.height <= childPosition.y)
				{
					//Debug.Log("out of range from map(child position:x" + childPosition.x + ", y:" + childPosition.y +")");
					continue;
				}

				Tile tile = GameManager.Instance.map.GetTile(childPosition.x, childPosition.y);
				if(Tile.Type.Floor != tile.type)
				{
					//Debug.Log("not tile(child position:x" + childPosition.x + ", y:" + childPosition.y +")");
					continue;
				}

				Node childNode = new Node(finder, this, childPosition, path_cost+1);
				if(finder.closeNodes.ContainsKey(childNode.id))
				{
					//Debug.Log ("already close node(child position:x" + childPosition.x + ", y:" + childPosition.y +")");
					continue;
				}

				if(finder.openNodes.ContainsKey(childNode.id))
				{
					Node openNode = finder.openNodes[childNode.id];
					if(openNode.path_cost < path_cost)
					{
						this.path_cost = openNode.path_cost + 1;
						this.parent = openNode;
						continue;
					}
				}
				else {
					finder.openNodes.Add(childNode.id, childNode);
				}
				children.Add (childNode);
			}
			children.Sort (delegate (Node lhs, Node rhs) {
				return lhs.cost.CompareTo(rhs.cost);
			});

			foreach (Node child in children) {
				if(null != child.FindNextPath())
				{
					return child.position;
				}
			}
			//Debug.Log ("cant find destination(x:" + position.x + ", y:" + position.y + ")");
			return null;
		}
	}

	public Object.Position FindNextPath(Object.Position start, Object.Position dest)
	{
		destination = dest;
		Node node = new Node (this, null, start, 0);
		Object.Position position = node.FindNextPath ();
		if (null != position) {
			Debug.Log ("find destination(x:" + position.x + ", y:" + position.y + ")");
			return position;
		}
		return null;
	}
}

public class PathFind
{
	public PathFind_AStar impl = new PathFind_AStar();
};