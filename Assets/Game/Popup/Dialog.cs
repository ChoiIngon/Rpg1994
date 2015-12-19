using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
	public Button submit;
	public Button cancel;
	public Text content;
	public RectTransform rect;
	// Use this for initialization
	void Start () {
		cancel.onClick.AddListener (() => {
			GameObject.Destroy(gameObject);
		});
	}
	
	public void AddSubmitListener(UnityEngine.Events.UnityAction call)
	{
		submit.onClick.AddListener(call);
	}

	public void SetWidth(int width)
	{
		rect.sizeDelta = new Vector2 (width, rect.rect.height);
	}
	public void SetText(string text)
	{
		content.text = text;
		rect.sizeDelta = new Vector2 (rect.rect.width, content.preferredHeight + 80);
	}
}
