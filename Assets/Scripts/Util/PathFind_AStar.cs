using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFind_AStar {
	/*
	public Dictionary<int, Node> openNodes = new Dictionary<int, Node>();
	public Dictionary<int, Node> closeNodes = new Dictionary<int, Node>();
	public Position destination;
	public class Node {
		public int id {
			get {
				return position.x + position.y * Map.Instance.width;
			}
		}
		public int cost {
			get {
				return path_cost + expect_cost;
			}
		}
		public int path_cost = 0;
		public int expect_cost = 0;
		public Position position;
		public Node parent = null;
		private PathFind_AStar finder;

		public Node(PathFind_AStar finder, Node parent, Position position, int path_cost)
		{
			this.finder = finder;
			this.position = position;
			this.expect_cost += Mathf.Abs(finder.destination.x - position.x);
			this.expect_cost += Mathf.Abs(finder.destination.y - position.y);
			this.path_cost = path_cost;
			this.parent = parent;
		}

		public Position FindNextPath() {
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

			List<Position> childPositions = new List<Position> ();
			childPositions.Add(new Position(position.x-1, position.y)); 
			childPositions.Add(new Position(position.x+1, position.y)); 
			childPositions.Add(new Position(position.x, position.y-1)); 
			childPositions.Add(new Position(position.x, position.y+1)); 

			List<Node> children = new List<Node> ();
			foreach (Position childPosition in childPositions) {
				if(0 > childPosition.x || 0 > childPosition.y || Map.Instance.width <= childPosition.x || Map.Instance.height <= childPosition.y)
				{
					//Debug.Log("out of range from map(child position:x" + childPosition.x + ", y:" + childPosition.y +")");
					continue;
				}

				Tile tile = Map.Instance.GetTile(childPosition.x, childPosition.y);
				if(1.0 < tile.GetObjectSize())
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

	public Position FindNextPath(Position start, Position dest)
	{
		destination = dest;
		Node node = new Node (this, null, start, 0);
		Position position = node.FindNextPath ();
		if (null != position) {
			Debug.Log ("find destination(x:" + position.x + ", y:" + position.y + ")");
			return position;
		}
		return null;
	}
	*/
}

public class PathFind
{
	public PathFind_AStar impl = new PathFind_AStar();
};