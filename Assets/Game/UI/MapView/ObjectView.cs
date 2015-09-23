using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectView : MonoBehaviour {
	public Object targetObject = null;

	public virtual void SetObject(Object o, string display) {
		targetObject = o;
		o.view = this;
		Text text = gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text>();
		text.text = display;
	}

	public virtual void Destroy() {
		transform.SetParent (null);
		GameObject.Destroy (gameObject);
	}
	
	public virtual void Update() {
		transform.localPosition = new Vector3(targetObject.position.x * MapView.TILE_SIZE, -targetObject.position.y * MapView.TILE_SIZE, 0);
	}
}
