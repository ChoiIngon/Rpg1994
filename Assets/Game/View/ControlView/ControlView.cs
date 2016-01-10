using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnWest() {
		Player.Instance.MoveTo(Character.DirectionType.West);
	}
	public void OnEast() {
		Player.Instance.MoveTo(Character.DirectionType.East);
	}
	public void OnNorth() {
		Player.Instance.MoveTo(Character.DirectionType.North);
	}
	public void OnSouth() {
		Player.Instance.MoveTo(Character.DirectionType.South);
	}
}

