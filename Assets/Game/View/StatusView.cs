using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusView : MonoBehaviour {
	public Text player;
	public AttributeView health;
	public AttributeView stamina;
	public Text location;

	// Update is called once per frame
	void Update () {
		player.color = Color.green;
		if (GameManager.Instance.player.health.max / 3 > GameManager.Instance.player.health) {
			player.color = Color.red;
		}
		health.Value = ":" + StatusBar (GameManager.Instance.player.health, GameManager.Instance.player.health.max, "#FF0000");
		stamina.Value = ":" + StatusBar (GameManager.Instance.player.stamina, GameManager.Instance.player.stamina.max, "#3BB9FF");
		location.text = GameManager.Instance.map.name;
	}

	string StatusBar(int current, int max, string color) {
		float rate = (float)current / (float)max;
		string bar = "<color=" + color + ">";
		int count = (int)(20 * rate);

		for (int i=0; i<count; i++) {
			bar += "=";
		}
		bar += "</color>";

		for (int i=count; i<20; i++) {
			bar += "=";
		}
		return bar;
	}
}
