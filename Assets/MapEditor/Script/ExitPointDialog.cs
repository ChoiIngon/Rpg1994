using UnityEngine;
using System.Collections;

public class ExitPointDialog : SingletonBehaviour<ExitPointDialog> {
	public string NextMapID;
	public string StartPointID;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnSubmit() {
		gameObject.SetActive (false);
	}

	public void OnCancel() {
		gameObject.SetActive (false);
	}
}
