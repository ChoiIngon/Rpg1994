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
		if (null == GameManager.Instance.player) {
			return;
		}
		player.color = Color.green;
		if (GameManager.Instance.player.health.max / 3 > GameManager.Instance.player.health) {
			player.color = Color.red;
		}
		health.Value = StatusBar (GameManager.Instance.player.health, GameManager.Instance.player.health.max, "#FF0000");
		stamina.Value = StatusBar (GameManager.Instance.player.stamina, GameManager.Instance.player.stamina.max, "#3BB9FF");
		location.text = GameManager.Instance.map.name;
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
