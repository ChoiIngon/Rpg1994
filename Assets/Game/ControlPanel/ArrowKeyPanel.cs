using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowKeyPanel : MonoBehaviour {
	public Transform [] arrows = new Transform[4];
	// Use this for initialization
	public float fillAmount = 0.0f;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		arrows [0].GetComponent<Image> ().fillAmount = fillAmount;
	}
}