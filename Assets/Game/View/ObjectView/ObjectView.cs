using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectView : MonoBehaviour {
	public Object targetObject = null;

	public virtual void SetObject(Object o, string display, Color color) {
		targetObject = o;
		o.view = this;
		Text text = gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text>();
		text.text = display;
		text.color = color;
	}

	public virtual void Destroy() {
		transform.SetParent (null);
		GameObject.Destroy (gameObject);
	}
	
	public virtual void Update() {
		SetVisible (targetObject.visible);
		transform.localPosition = new Vector3(targetObject.position.x * MapView.TILE_SIZE, -targetObject.position.y * MapView.TILE_SIZE, 0);
	}

	public virtual void SetVisible(bool value) {
		Text text = gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text>();
		if (true == value) {
			text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
		} else {
			text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
		}
	}
}
