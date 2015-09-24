using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TextContents : MonoBehaviour {
	public float width = 0;

	public void Add(Text text)
	{
		if (0 < transform.childCount) {
			Text child = transform.GetChild (transform.childCount - 1).GetComponent<Text>();
			if(null != child)
			{
				child.text += text.text;
				Destroy(text.gameObject);
			}
			else
			{
				text.transform.SetParent (transform, false);
			}
		}
		else {
			text.transform.SetParent (transform, false);
		}

		width += text.preferredWidth;
	}
	public void Add(Button button) {
		Text text = button.GetComponentInChildren<Text>();
		button.transform.SetParent (transform, false);
		width += text.preferredWidth;
	}
}
