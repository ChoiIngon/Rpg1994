using UnityEngine;
using System.Collections;

public class ExitPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnYes() {
		Application.Quit ();
	}
	public void OnNo() {
		gameObject.SetActive (false);
	}
}
