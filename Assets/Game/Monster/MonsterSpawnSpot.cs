using UnityEngine;
using System.Collections;

public class MonsterSpawnSpot {
	public string id;
	public MonsterData monster;
	public int interval;
	public int deadTime;
	public int count; // zero means infinity
	public Object.Position position;

    public MonsterSpawnSpot()
    {
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

		if (Player.Instance.sight > Vector2.Distance (position, Player.Instance.position)) {
			return;
		}
		if (null == monster && Player.Instance.position != position && (deadTime + interval < GameManager.Instance.currentTurn || 0 == deadTime)) {
			monster = MonsterManager.Instance.CreateInstance (id);
			monster.SetPosition(position);
		}
	}
}
