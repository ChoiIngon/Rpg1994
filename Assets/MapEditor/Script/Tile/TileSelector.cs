using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MapEditor {
	public class TileSelector : MonoBehaviour {
		public static TileSelector selected;
		private string _type;
		public string type {
			get { return _type; }
		}
		void Start() {
			_type = name;
		}
		public void OnClick() {
			TileSelector.selected = this;
			Debug.Log (this.name + " tile selected");
		}
	}
}