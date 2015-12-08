using UnityEngine;
using System.Collections;

public class PlayerInfoView : Util.UI.Singleton<PlayerInfoView> { 

	public void Init ()
	{
		transform.localScale = new Vector3 (0, 0, 0);
		gameObject.SetActive (false);
	}

	public void OnOpen()
	{
		gameObject.SetActive (true);
		gameObject.GetComponent<Animation>().Play();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
