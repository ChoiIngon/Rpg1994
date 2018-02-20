using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusView : MonoBehaviour {
	private const int BAR_WIDTH = 20;
	public Text player;
	public AttributeView health;
	public AttributeView stamina;
	public Text location;

	// Update is called once per frame
	void Update () {
		/*
		if (null == Player.Instance) {
			return;
		}
		player.color = Color.green;
		if (Player.Instance.health.max / 3 > Player.Instance.health) {
			player.color = Color.red;
		}
		health.Value = StatusBar (Player.Instance.health, Player.Instance.health.max, "#FF0000");
		stamina.Value = StatusBar (Player.Instance.stamina, Player.Instance.stamina.max, "#3BB9FF");
		location.text = Map.Instance.name;
		*/
	}

	string StatusBar(int current, int max, string color) {
		float rate = (float)current / (float)max;
		string bar = "<color=" + color + ">";
		int count = (int)(BAR_WIDTH * rate);

		for (int i=0; i<count; i++) {
			bar += "=";
		}
		bar += "</color>";

		for (int i=count; i<BAR_WIDTH; i++) {
			bar += "=";
		}
		return bar;
	}
}
