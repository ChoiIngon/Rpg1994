using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using SimpleJSON;

namespace MapEditor {
	public class Tile : MonoBehaviour, IPointerClickHandler {
		public Position position;
		public TileImpl impl;
		private Text _text;

		public void Init() {
			Transform trans = transform.Find ("Text");
			if (null == trans) {
				throw new System.Exception ("can't find Text object");
			}
			impl = null;
			_text = trans.GetComponent<Text>();
			position = new Position (0, 0);
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

		public void OnPointerClick(PointerEventData eventData) {
			if (PointerEventData.InputButton.Left == eventData.button) {
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
					Debug.Log ("error:" + e.Message);
					impl = tmp;
				}
			} else if (PointerEventData.InputButton.Right == eventData.button) {
				if(null == impl)
				{
					return;
				}
				impl.EditDialog();
			}
		}

		public JSONNode ToJSON() {
			if (null == impl) {
				return null;
			}
			return impl.ToJSON (this);
		}

		public void FromJSON(JSONNode node) {
			impl = TileImplFactory.Instance.Create (node["type"]);
			impl.FromJSON (this, node);
		}
	}
}