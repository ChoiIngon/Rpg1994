using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MapEditor {
	public class TileSelector : MonoBehaviour {
		public static TileSelector selected;
		public string type;
		public string text;
		public Color color;
		void Start() {
			type = name;
		}
		public void OnClick() {
			TileSelector.selected = this;
			Debug.Log (this.name + " tile selected");
		}
	}
}