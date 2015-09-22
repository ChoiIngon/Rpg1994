using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectView : MonoBehaviour {
	public Object targetObject = null;
	public void SetObject(Object o, string display) {
		targetObject = o;
		o.view = this;

		Text text = gameObject.transform.FindChild ("Text").gameObject.GetComponent<Text>();
		text.text = display;
	}

	public void Destroy() {
		MapView.Instance.SetActivate (targetObject.position.x, targetObject.position.y, true);
		transform.SetParent (null);
		GameObject.Destroy (gameObject);
	}
	
	private void Update() {
		MapView.Instance.SetActivate (targetObject.position.x, targetObject.position.y, false);
		transform.localPosition = new Vector3(targetObject.position.x * MapView.TILE_SIZE, -targetObject.position.y * MapView.TILE_SIZE, 0);
	}
}
