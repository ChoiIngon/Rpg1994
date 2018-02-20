using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterData : Character {
	/*
	public enum State
	{
		Idle,
		Chase,
		Fight,
		Die
	}
	
	public int seq;
	public MonsterInfo info;
	public State state;
	public ObjectView view;
	public RewardData reward;

	public MonsterData() {
		category = Object.Category.Monster;
	}
	
	public virtual void Idle() {
		this.direction = (Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max);
		Move (direction);
		state = State.Idle;
	}
	public virtual void Move() {
		float chaseWeight = (float)speed / Player.Instance.speed;
		if (UnityEngine.Random.Range(1, 100) < chaseWeight * 100) {
			PathFind_AStar path = new PathFind_AStar ();
			Position next = path.FindNextPath (position, Player.Instance.position);
			if (null != next) {
				Move (next);
			}
		}
		state = State.Chase;
	}

	public virtual void Attack() {
		Attack(Player.Instance);
		state = State.Fight;
	}

	public override void Destroy() {
		state = State.Die;

		Player.Instance.inventory.gold += reward.gold;
		Player.Instance.exp += reward.exp;
		foreach (ItemData item in reward.items) {
			CreateItemStack (item, new Position (position.x, position.y));
			OnDropItem (item);
		}

		if (null != QuestManager.Instance.triggerKillMonster) {
			QuestManager.Instance.triggerKillMonster (info.id);
		}
		base.Destroy();
	}

	public override void OnCreate() {
		view = ObjectView.Create<ObjectView> (this, "M", Color.red);
	}

	public override void OnMove(Character.DirectionType direction)
	{
		view.SetPosition(position);
	}

	public override void OnAttack(Character defender) {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>이(가) 당신을 공격합니다.");
	}
	public override void OnDodge(Character attacker) {
		LogView.Instance.Write (name + " dodge your attack");
	}
	public override void OnDamage(Character attacker, int damage) {
		Tile tile = Map.Instance.GetTile (position.x, position.y);
		tile.view.CreateFloatingMessage ("-" + damage.ToString(), Color.yellow);
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>은(는) " + damage + "의 피해를 입었습니다.");
	}
	public override void OnDestroy() {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color> 은(는) 죽었습니다.");
		LogView.Instance.Write ("You get " + reward.exp + " exp");
		LogView.Instance.Write ("You get " + reward.gold + " gold");
		view.OnDestroy ();
	}

	public override void OnTrigger(Object obj) {
		Player.Instance.Attack (this);
	}

	public override void Update() {
		base.Update ();
		if (true == IsVisible (Player.Instance)) {
			if(range >= Vector2.Distance (position, Player.Instance.position)) {
				Attack ();
				return;
			}
			else {
				Move ();
				return;
			}
		}
		Idle ();
	}
	*/
}

