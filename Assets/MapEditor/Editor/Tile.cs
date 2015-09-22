using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

namespace MapEditor {
	public class Tile : MonoBehaviour {
		public enum Type {
			Unused,
			Floor,
			Corridor,
			Wall,
			ClosedDoor,
			OpenDoor,
			UpStairs,
			DownStairs,
			Max
		}

		public Type type;
		public Text GetTileText()
		{
			Button button = transform.GetComponent<Button> ();
			Transform txtTrans = button.transform.FindChild ("Text");
			if (null == txtTrans) {
				throw new System.Exception ("can't find Text object");
			}
			return txtTrans.GetComponent<Text>();
		}
		public void SetTileText(string t)
		{
			Button button = transform.GetComponent<Button> ();
			Transform txtTrans = button.transform.FindChild ("Text");
			if (null == txtTrans) {
				throw new System.Exception ("can't find Text object");
			}
			Text text = txtTrans.GetComponent<Text>();
			text.text = t;
		}
		public void SetTileColor(Color color)
		{
			Button button = transform.GetComponent<Button> ();
			Transform txtTrans = button.transform.FindChild ("Text");
			if (null == txtTrans) {
				throw new System.Exception ("can't find Text object");
			}
			Text text = txtTrans.GetComponent<Text>();
			text.color = color;
		}
		public Type GetTileType () {
			return type;
		}

		public void OnClick()
		{
			if (null == Selection.activeObject) {
				return;
			}

			GameObject obj = (GameObject)Instantiate<UnityEngine.Object>(Selection.activeObject);
			Tile tile = obj.transform.GetComponent<Tile>();

			if (null == tile) {
				return;
			}

			Button button = transform.GetComponent<Button>();
			Transform child = button.transform.FindChild ("Text");
			Text t1 = child.transform.GetComponent<Text> ();
			Text t2 = tile.GetTileText ();
			t1.text = t2.text;
			t1.color = t2.color;
			type = tile.GetTileType();

			Destroy(obj);
		}
	}
}