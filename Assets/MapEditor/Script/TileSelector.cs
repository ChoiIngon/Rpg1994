using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MapEditor {
	public class TileSelector : MonoBehaviour {
		public static TileSelector selected;
		public Tile.Type type {
			get {
				return type;
			}
		}
		// Use this for initialization
		//private Button selfButton;
		//private Text selfText;
		void Start () {
			/*
			selfButton = GetComponent<Button> ();
			if (null == selfButton) {
				throw new System.Exception("no `Button` component");
			}
			selfText = selfButton.transform.FindChild("Text").GetComponent<Text>();
			if (null == selfText) {
				throw new System.Exception("no `Text` component");
			}
			*/
		}
		
		// Update is called once per frame
		void Update () {
		}
	
		public void OnClick() {
			TileSelector.selected = this;
			Debug.Log (this.name + " tile selected");
		}
	}
}