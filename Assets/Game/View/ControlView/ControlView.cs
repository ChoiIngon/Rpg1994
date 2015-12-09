using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnAttack() {
		try {
			GameManager.Instance.player.Attack ();
			MapView.Instance.Center();
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}

	}

	public void OnShowInventory() {
		//((PlayerView)GameManager.Instance.player.view).ShowInventory ();
		//InventoryView view = new InventoryView (GameManager.Instance.player.inventory);
	}

	public void OnWest() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.West);
			//MapView.Instance.SetMapScale(2.0f);
			MapView.Instance.Center();
			LogView.Instance.Write ("서쪽으로 이동합니다(" + GameManager.Instance.player.position.x + ", " + GameManager.Instance.player.position.y + ")");
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnEast() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.East);
			//MapView.Instance.SetMapScale(2.0f);
			MapView.Instance.Center();
			LogView.Instance.Write ("동쪽으로 이동합니다(" + GameManager.Instance.player.position.x + ", " + GameManager.Instance.player.position.y + ")");
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnNorth() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.North);
			MapView.Instance.Center();
			LogView.Instance.Write ("북쪽으로 이동합니다(" + GameManager.Instance.player.position.x + ", " + GameManager.Instance.player.position.y + ")");
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
	public void OnSouth() {
		try {
			GameManager.Instance.player.MoveTo(Character.DirectionType.South);
			MapView.Instance.Center();
			LogView.Instance.Write ("남쪽으로 이동합니다(" + GameManager.Instance.player.position.x + ", " + GameManager.Instance.player.position.y + ")");
		}
		catch(System.Exception e) {
			LogView.Instance.Write (e.Message);
		}
	}
}

