using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnWest() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.West);
		MapView.Instance.Center();
	}
	public void OnEast() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.East);
		MapView.Instance.Center();
	}
	public void OnNorth() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.North);
		MapView.Instance.Center();
	}
	public void OnSouth() {
		GameManager.Instance.player.MoveTo(Character.DirectionType.South);
		MapView.Instance.Center();
	}
}

