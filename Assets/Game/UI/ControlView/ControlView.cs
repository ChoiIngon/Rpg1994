using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnAttack() {
		try {
			Game.Instance.player.Attack ();
		}
		catch(System.Exception e) {
			ScrollView.Instance.Add (e.Message + "\n");
		}
	}

	public void OnShowInventory() {
		MapView.Instance.SetMapScale(1.0f);
		((PlayerView)Game.Instance.player.view).ShowInventory ();
	}

	public void OnWest() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.West);
			MapView.Instance.SetMapScale(2.0f);
			ScrollView.Instance.Add ("you moved to west(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			ScrollView.Instance.Add (e.Message + "\n");
		}
	}
	public void OnEast() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.East);
			MapView.Instance.SetMapScale(2.0f);
			ScrollView.Instance.Add ("you moved to east(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			ScrollView.Instance.Add (e.Message + "\n");
		}
	}
	public void OnNorth() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.North);
			MapView.Instance.SetMapScale(2.0f);
			ScrollView.Instance.Add ("you moved to north(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			ScrollView.Instance.Add (e.Message + "\n");
		}
	}
	public void OnSouth() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.South);
			MapView.Instance.SetMapScale(2.0f);
			ScrollView.Instance.Add ("you moved to south(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			ScrollView.Instance.Add (e.Message + "\n");
		}
	}
}

