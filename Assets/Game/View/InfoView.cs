using UnityEngine;
using System.Collections;

public class InfoView : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public static void MonsterInfo(MonsterData data) {
		MonsterInfo info = data.info;
		LogView.Title(info.name);
		LogView.Text(info.description + "\n\n");
		LogView.Text("hp:" + data.health.current + "/" + data.health.max + "\n");
	}
}
