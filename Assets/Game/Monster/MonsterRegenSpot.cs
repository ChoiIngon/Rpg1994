using UnityEngine;
using System.Collections;

public class MonsterRegenSpot {
	public string id;
	public MonsterData monster;
	public int interval;
	public int deadTime;
	public int count; // zero means infinity
	public Object.Position position;

	public MonsterRegenSpot() {
		id = "";
		monster = null;
		interval = 0;
		deadTime = 0;
		count = 0;
		position = new Object.Position (0, 0);
	}

	public void Update() {
		if (null != monster && MonsterData.State.Die == monster.state) {
			monster = null;
			deadTime = GameManager.Instance.currentTurn;
		}

		if (null == monster && GameManager.Instance.player.position != position && (deadTime + interval < GameManager.Instance.currentTurn || 0 == deadTime)) {
			monster = MonsterManager.Instance.CreateInstance (id);
			monster.SetPosition(new Object.Position (position.x, position.y));
			monster.OnCreate();
		}
	}
}
