using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace View {
	public class ObjectView : MonoBehaviour {
		public Text display;
		public Object.Position position;

		public ObjectView() {
			Transform tileTextTransform = transform.FindChild ("Text");
			if (null == tileTextTransform) {
				throw new System.Exception ("can't find text object");
			}
			display = tileTextTransform.GetComponent<Text> ();
		}
		public virtual void SetVisible(bool value) {
			gameObject.SetActive (value);
		}

		public static T Create<T>(Object obj, string text, Color color) where T : ObjectView {
			// "Prefab/Map/ObjectView"
			GameObject objectView = GameObject.Instantiate(Resources.Load("Prefab/Map/ObjectView", typeof(GameObject)) ) as GameObject;
			objectView.AddComponent<T> ();
			T tView = objectView.GetComponent<T> ();
			tView.name = text;
			tView.display.text = text;
			tView.display.color = color;
			tView.position = obj.position;
			return tView;
		}

		public static T Create<T>(Object.Position position, string text, Color color) where T : ObjectView {
			// "Prefab/Map/ObjectView"
			GameObject objectView = GameObject.Instantiate(Resources.Load("Prefab/Map/ObjectView", typeof(GameObject)) ) as GameObject;
			objectView.AddComponent<T> ();
			T tView = objectView.GetComponent<T> ();
			tView.name = text;
			tView.display.text = text;
			tView.display.color = color;
			tView.position = position;
			return tView;
		}
	}
}