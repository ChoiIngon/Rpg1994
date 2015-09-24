using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class LogView : SingletonBehaviour<LogView> {
	public Text 		textPref;
	public Button 		buttonPref;
	public TextContents textContentsPref;

	private float CONTENTS_WIDTH = 0;
	public int MAX_LINE_COUNT = 100;
	private TextContents current = null;
	private Regex regex = new Regex("[ \\t\\S]+|\\n");

	void Start() {
		Transform contents = transform.FindChild ("Contents");
		if (null == contents) {
			throw new System.Exception ("can't find contents object");
		}
		RectTransform rectTransform = contents.GetComponent<RectTransform> ();
		CONTENTS_WIDTH = rectTransform.rect.width;
		current = Instantiate<TextContents>(textContentsPref);
	}

	public void Add(string s, UnityAction handler = null)
	{
		Transform contents = transform.FindChild ("Contents");
		MatchCollection matches = regex.Matches (s);

		for (int i=0; i<matches.Count; i++) {
			if("\n" == matches[i].Value)
			{
				current.transform.SetParent(contents, false);
				current = Instantiate<TextContents>(textContentsPref);

				if(0 == i)
				{
					Text text = Instantiate<Text> (textPref);
					text.text = "";
					current.Add (text);
					current.transform.SetParent(contents, false);
					current = Instantiate<TextContents>(textContentsPref);
				}
				continue;
			}
			if (null == handler) {
				Text text = Instantiate<Text> (textPref);
				text.text = matches[i].Value;
				current.Add (text);
			} else {
				Button button = Instantiate<Button>(buttonPref);
				button.onClick.AddListener(handler);
				Text text = button.GetComponentInChildren<Text>();
				if(0 < current.width)
				{
					text.text = " <b>" + matches[i].Value + "</b>";
				}
				else{
					text.text = "<b>" + matches[i].Value + "</b>";
				}
				if(null == current || CONTENTS_WIDTH < current.width + text.preferredWidth)
				{
					current.transform.SetParent(contents, false);
					current = Instantiate<TextContents>(textContentsPref);
					text.text = "<b>" + matches[i].Value + "</b>";;
				}
				current.Add (button);
			}
		}
		while (this.MAX_LINE_COUNT < contents.childCount) {
			Transform line = contents.GetChild(0);
			line.SetParent(null);
			GameObject.Destroy(line.gameObject);
		}
		GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
	}
	public static string MakeFixedLengthText(string text, int length) {
		string s = text;
		if (s.Length >= length) {
			return s.Substring(0, length);
		}
		for(int i=s.Length; i<=length; i++)
		{
			s += " ";
		}
		return s;
	}
	public void AddTitle(string s) {
		if (20 < s.Length) {
			throw new System.Exception("title text is too long(" + s + ")");
		}
		GameObject view = GameObject.Instantiate(Resources.Load("Prefab/LogView/Title", typeof(GameObject)) ) as GameObject;
		Transform contents = transform.FindChild ("Contents");
		view.transform.SetParent (contents, false);
		Text text = view.transform.FindChild ("Text").GetComponent<Text>();
		text.text = "<b>" + s + "</b>";
	}

	public static void Write(string s) {
		LogView.Instance.Add (s);
	}

	public static void Title(string s) {
		LogView.Instance.AddTitle (s);
	}
	public static void Button(string s, UnityAction handler) {
		LogView.Instance.Add (s, handler);
	}
}
