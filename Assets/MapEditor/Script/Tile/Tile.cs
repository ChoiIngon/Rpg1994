using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor {
	public class Tile : MonoBehaviour {
		public Object.Position position;
		public TileImpl impl;
		private Text _text;

		public void Init() {
			Transform trans = transform.FindChild ("Text");
			if (null == trans) {
				throw new System.Exception ("can't find Text object");
			}
			impl = null;
			_text = trans.GetComponent<Text>();
			position = new Object.Position (0, 0);
		}

		public string type {
			get {
				if(null == impl)
				{
					return "";
				}
				return impl.GetType (); 
			}
		}
		public string text {
			get { return _text.text; }
			set { _text.text = value; }
		}
		public Color color {
			get { return _text.color; }
			set { _text.color = value; }
		}
		public void SetText() {
			if (null == impl) {
				return;
			}
			impl.SetText (this);
		}
		public void OnClick()
		{
			if (Input.GetMouseButtonDown (0)) {
				if (null == TileSelector.selected) {
					return;
				}
				TileSelector selector = TileSelector.selected;
				TileImpl tmp = impl;
				try {
					impl = TileImplFactory.Instance.Create(selector.type);
					SetText ();
				}
				catch(System.Exception e)
				{
					Debug.Log (e.Message);
					impl = tmp;
				}
			} else if (Input.GetMouseButtonDown (1)) {
				impl.EditDialog();
			}
		}
	}
}