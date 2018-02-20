using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributeView : MonoBehaviour {
	private Text _name;
	private Text _value;

	public string Name
	{
		set {
			_name.text = value;
		}
		get {
			return _name.text;
		}
	}
	public string Value {
		set {
			if(null == _value)
			{
				_value = transform.Find("Value").GetComponent<Text>();
			}
			_value.text = value;
		}
		get {
			return _value.text;
		}
	}
	// Use this for initialization
	void Start () {
		_name = transform.Find ("Name").GetComponent<Text> ();
		_value = transform.Find ("Value").GetComponent<Text> ();
	}
}
