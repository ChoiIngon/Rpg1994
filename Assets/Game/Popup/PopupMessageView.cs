using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupMessageView : Util.UI.Singleton<PopupMessageView> {
	public Button submit;
	public Button cancel;
	public Button confirm;
	public Text content;
	public RectTransform rect;

	// Use this for initialization
	void Start () {
		transform.localPosition = Vector3.zero;
		gameObject.SetActive (false);

		submit.gameObject.SetActive (false);
		cancel.gameObject.SetActive (false);
		cancel.onClick.AddListener (Close);

		confirm.gameObject.SetActive (false);
		confirm.onClick.AddListener (Close);
	}

	public void Close() {
		content.text = "";
		submit.onClick.RemoveAllListeners();
		submit.gameObject.SetActive(false);
		cancel.gameObject.SetActive (false);
		confirm.gameObject.SetActive (true);
		gameObject.SetActive(false);
	}

	public void AddSubmitListener(UnityEngine.Events.UnityAction call)
	{
		submit.gameObject.SetActive(true);
		cancel.gameObject.SetActive (true);
		confirm.gameObject.SetActive (false);
		submit.onClick.AddListener(call);
		submit.onClick.AddListener(Close);
	}

	public void SetWidth(int width)
	{
		rect.sizeDelta = new Vector2 (width, rect.rect.height);
	}

	public void SetText(string text, TextAnchor alinement = TextAnchor.MiddleLeft)
	{
		content.text = text;
		content.alignment = alinement;
		rect.sizeDelta = new Vector2 (rect.rect.width, content.preferredHeight+100);
	}
}
