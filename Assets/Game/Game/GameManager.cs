using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Game.Instance.Init ();
		LogView.Title ("RPG 1994");
		LogView.Text ("Welcome to <b><color=#FF0000>Text</color></b> <b><color=#00FF00>RPG</color></b> <b><color=#0000FF>World</color></b>\n\n");
		LogView.Text ("All of this world are consisted of only text. ");
		LogView.Text ("Draw all things in your head.\n");
		LogView.Text ("\n");
		LogView.Text ("Well...now we start..Good luck son.\n");
		LogView.Map ();
		LogView.Title (Game.Instance.map.name);
		LogView.Text (Game.Instance.map.description + "\n");
	}
	
	// Update is called once per frame
	void Update () {
		Game.Instance.Update ();
	}
}
