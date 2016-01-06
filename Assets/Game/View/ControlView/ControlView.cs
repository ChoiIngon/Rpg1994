using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnWest() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.West);
	}
	public void OnEast() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.East);
	}
	public void OnNorth() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.North);
	}
	public void OnSouth() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.South);
	}
}

