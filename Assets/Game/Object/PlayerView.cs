using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerView : ObjectView {

	public override void SetVisible (bool value)
	{
		gameObject.SetActive (true);
	}
}
