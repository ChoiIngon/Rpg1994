using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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
	public virtual void SetPosition(Object.Position position) {
		this.position = new Object.Position (position.x, position.y);
	}
}
