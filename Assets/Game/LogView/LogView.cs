using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class LogView : Util.UI.Singleton<LogView> {
	public Text textPref;
	public Transform contents;
	public const int MAX_LINE_COUNT = 30;
	void Start() {
		contents = transform.FindChild ("Contents");
		if (null == contents) {
			throw new System.Exception ("can't find contents object");
		}
	}

	public void Write(string s)
	{
		Text text = Instantiate<Text> (textPref);
		text.text = s;
		text.transform.SetParent (contents, false);
	
		while (MAX_LINE_COUNT < contents.childCount) {
			Transform line = contents.GetChild(0);
			line.SetParent(null);
			GameObject.Destroy(line.gameObject);
		}
		GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
	}
}
