using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MapEditor {
	public class EnterPointDialog : Util.UI.Singleton<EnterPointDialog> {
		public EnterPoint tile;
		public EnterPointDialog()
		{
			Debug.Log ("<<< create EnterPointDialog >>>");
		}
		public string name {
			set {
				InputField input = transform.FindChild ("NameInput").GetComponent<InputField>();
				input.text = value;
			}
			get {
				InputField input = transform.FindChild ("NameInput").GetComponent<InputField>();
				return input.text;
			}
		}
		public string description
		{
			set {
				InputField input = transform.FindChild ("DescInput").GetComponent<InputField>();
				input.text = value;
			}
			get {
				InputField input = transform.FindChild ("DescInput").GetComponent<InputField>();
				return input.text;
			}
		}

		// Use this for initialization
		void Start () {
			Debug.Log ("EnterPointDialog");
			//transform.FindChild ("NameInput").GetComponent<InputField>().onEndEdit.AddListener(SubmitName);  // This also works
			//transform.FindChild ("DescInput").GetComponent<InputField>().onEndEdit.AddListener(SubmitDescription);  // This also works

			//name = transform.FindChild ("NameInput").FindChild("Placeholder").GetComponent<Text>();
			//description = transform.FindChild ("DescInput").FindChild ("Placeholder").GetComponent<Text>();
			tile = null;
		}
		/*
		private void SubmitName(string text)
		{
			if (null == tile) {
				return;
			}
			tile.name = text;
		}
		private void SubmitDescription(string text)
		{
			if (null == tile) {
				return;
			}
			tile.description = text;
		}
		*/
		// Update is called once per frame
		void Update () {}

		public void OnSubmit() {
			if (null == tile) {
				Debug.Log ("empty tile");
				return;
			}
			tile.name = name;
			tile.description = description;
			gameObject.SetActive (false);
		}

		public void OnCancel() {
			tile = null;
			gameObject.SetActive (false);
		}
	}
}