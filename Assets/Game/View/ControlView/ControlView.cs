﻿using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnAttack() {
		try {
			PathFind_AStar path = new PathFind_AStar();
			Object.Position next = path.FindNextPath(GameManager.Instance.player.position, new Object.Position(25, 5));
			if(null != next)
			{
				GameManager.Instance.player.SetPosition(next);
			}
			GameManager.Instance.player.Attack ();
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}

	public void OnWest() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.West);
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnEast() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.East);
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnNorth() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.North);
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnSouth() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.South);
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
}

