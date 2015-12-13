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

	public static T Create<T>(Object obj, string text, Color color) where T : ObjectView {
		return Create<T>(obj.position, text, color);
	}

	public static T Create<T>(Object.Position position, string text, Color color) where T : ObjectView {
		// "Prefab/Map/ObjectView"
		GameObject objectView = GameObject.Instantiate(Resources.Load("Prefab/Map/ObjectView", typeof(GameObject)) ) as GameObject;
		objectView.transform.FindChild("Text").GetComponent<Text> ().fontSize = MapView.TILE_SIZE;
		T tView = objectView.AddComponent<T> ();
		tView.name = text;
		tView.display.text = text;
		tView.display.color = color;
		tView.position = position;
		return tView;
	}

	public void CreateFloatingMessage(string text, Color color) {
		GameObject obj = GameObject.Instantiate(Resources.Load ("Prefab/Map/FloatingMessage", typeof(GameObject))) as GameObject;
		Text message = obj.GetComponent<Text> ();
		message.text = text;
		message.color = color;
		obj.transform.SetParent (MapView.Instance.tiles.GetChild(position.x + position.y * GameManager.Instance.map.width), false);
		StartCoroutine (WaitForAnimation (obj.GetComponent<Animator> ()));
	}

	IEnumerator WaitForAnimation(Animator animator)
	{
		while (false == animator.GetCurrentAnimatorStateInfo(0).IsName("FloatingMessage")) {
			yield return new WaitForEndOfFrame();
		}
		GameObject.Destroy (animator.gameObject, 2.0f);
	}
}