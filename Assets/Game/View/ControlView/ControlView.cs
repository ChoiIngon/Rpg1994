using UnityEngine;
using System.Collections;

public class ControlView : MonoBehaviour {
	public void OnAttack() {
		LogView.Map ();
		try {
			Game.Instance.player.Attack ();
		}
		catch(System.Exception e) {
			LogView.Text (e.Message + "\n");
		}

	}

	public void OnShowInventory() {
		//((PlayerView)Game.Instance.player.view).ShowInventory ();
		InventoryView view = new InventoryView (Game.Instance.player.inventory);
	}

	public void OnWest() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.West);
			//MapView.Instance.SetMapScale(2.0f);
			LogView.Map ();
			LogView.Text ("서쪽으로 이동합니다(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			LogView.Text (e.Message + "\n");
		}
	}
	public void OnEast() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.East);
			//MapView.Instance.SetMapScale(2.0f);
			LogView.Map ();
			LogView.Text ("동쪽으로 이동합니다(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			LogView.Text (e.Message + "\n");
		}
	}
	public void OnNorth() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.North);
			//MapView.Instance.SetMapScale(2.0f);
			LogView.Map ();
			LogView.Text ("북쪽으로 이동합니다(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			LogView.Text (e.Message + "\n");
		}
	}
	public void OnSouth() {
		try {
			Game.Instance.player.MoveTo(Character.DirectionType.South);
			//MapView.Instance.SetMapScale(2.0f);
			LogView.Map ();
			LogView.Text ("남쪽으로 이동합니다(" + Game.Instance.player.position.x + ", " + Game.Instance.player.position.y + ")\n");
		}
		catch(System.Exception e) {
			LogView.Text (e.Message + "\n");
		}
	}
}

