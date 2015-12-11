using UnityEngine;
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
		private PathFind_AStar finder;

		public Node(PathFind_AStar finder, Object.Position position, int path_cost)
		{
			this.finder = finder;
			this.position = position;
			this.expect_cost += Mathf.Abs(finder.destination.x - position.x);
			this.expect_cost += Mathf.Abs(finder.destination.y - position.y);
			this.path_cost = path_cost;
		}

		public Object.Position FindNextPath() {
			Debug.Log ("===== find path(x:" + position.x + ", y:" + position.y +"), " + expect_cost + "/" + path_cost + "/" + cost +" =====");
			if (true == finder.openNodes.ContainsKey (id)) {
				finder.openNodes.Remove (id);
			}
			finder.closeNodes.Add (id, this);

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

				Node childNode = new Node(finder, childPosition, path_cost+1);
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
						Debug.Log ("weight over(child position:x" + childPosition.x + ", y:" + childPosition.y +")");
						return null;
					}
				}
				else {
					finder.openNodes.Add(childNode.id, childNode);
					//Debug.Log ("insert into open list(x:" + childNode.position.x + ", y:" + childNode.position.y + ")"+ childNode.expect_cost + "/" + childNode.path_cost + "/" + childNode.cost +" =====");
				}
				if(finder.destination == childNode.position)
				{
					//Debug.Log ("find destination(x:" + childNode.position.x + ", y:" + childNode.position.y + "), return position(x:" + position.x + ", y:" + position.y + ")");
					return position;
				}
				children.Add (childNode);
			}
			children.Sort (delegate (Node lhs, Node rhs) {
				return lhs.cost.CompareTo(rhs.cost);
			});

			foreach (Node child in children) {
				if(null != child.FindNextPath())
				{
					//Debug.Log ("find destination(x:" + child.position.x + ", y:" + child.position.y + "), return position(x:" + position.x + ", y:" + position.y + ")");
					return child.position;
				}
			}
			Debug.Log ("cant find destination(x:" + position.x + ", y:" + position.y + ")");
			return null;
		}
	}

	public Object.Position FindNextPath(Object.Position start, Object.Position dest)
	{
		destination = dest;
		Node node = new Node (this, start, 0);
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