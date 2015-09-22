using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class ScrollView : SingletonBehaviour<ScrollView> {
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
		current.transform.SetParent(contents, false);
	}

	public void Add(string s, UnityAction handler = null)
	{
		Transform contents = transform.FindChild ("Contents");
		MatchCollection matches = regex.Matches (s);

		for (int i=0; i<matches.Count; i++) {
			if("\n" == matches[i].Value)
			{
				current = Instantiate<TextContents>(textContentsPref);
				current.transform.SetParent(contents, false);
				if(0 == i)
				{
					Text text = Instantiate<Text> (textPref);
					text.text = "";
					current.Add (text);
					current = Instantiate<TextContents>(textContentsPref);
					current.transform.SetParent(contents, false);
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
					current = Instantiate<TextContents>(textContentsPref);
					current.transform.SetParent(contents, false);
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
	public void AddTitle(string text) {
		const int width = 38;
		int first_space_count = (width - text.Length) / 2;
		string s = "|";
		for(int i=0; i<first_space_count-1; i++)
		{
			s += " ";
		}
		int second_space_count = width - first_space_count - text.Length;
		s += "<b>" + text + "</b>";
		for(int i=0; i<second_space_count-1; i++)
		{
			s += " ";
		}
		s += "|\n";

		string bar1 = "┌";
		for (int i=0; i<s.Length-10; i++) {
			bar1 += "─";
		}
		bar1 += "┐\n";
		Add (bar1);
		Add (s);
		string bar2 = "└";
		for (int i=0; i<s.Length-10; i++) {
			bar2 += "─";
		}
		bar2 += "┘\n";
		Add (bar2);
	}
}
