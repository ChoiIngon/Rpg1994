using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterData : Character {
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

	public override void Update() {
		if (true == IsVisible (GameManager.Instance.player.position)) {
			if(range >= Vector2.Distance (position, GameManager.Instance.player.position)) {
				Attack ();
				return;
			}
			else {
				Move ();
				return;
			}
		}
		Idle ();
		base.Update ();
	}
	public virtual void Idle() {
		this.direction = (Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max);
		Move (direction);
		state = State.Idle;
	}
	public virtual void Move() {
		float chaseWeight = (float)speed / GameManager.Instance.player.speed;
		if (UnityEngine.Random.Range(1, 100) < chaseWeight * 100) {
			PathFind_AStar path = new PathFind_AStar ();
			Object.Position next = path.FindNextPath (position, GameManager.Instance.player.position);
			if (null != next) {
				Move (next);
			}
		}
		if (null != view) {
			view.SetVisible (visible);
		}
		state = State.Chase;
	}
	public virtual void Attack() {
		Attack(GameManager.Instance.player);
		state = State.Fight;
	}

	public override void Destroy() {
		state = State.Die;

		GameManager.Instance.player.inventory.gold += reward.gold;
		GameManager.Instance.player.exp += reward.exp;
		foreach (ItemData item in reward.items) {
			CreateItemStack (item, new Object.Position (position.x, position.y));
			OnDropItem (item);
		}

		QuestManager.Instance.triggerKillMonster (info.id);
		MonsterManager.Instance.Remove (seq);
		base.Destroy();
	}

	public override void OnCreate() {
		view = ObjectView.Create<ObjectView> (this, "M", Color.red);
		view.SetVisible (false);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnMove(Character.DirectionType direction)
	{
		view.position = position;
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnAttack(Character defender) {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>이(가) 당신을 공격합니다.");
	}
	public override void OnDodge(Character attacker) {
		LogView.Instance.Write (name + " dodge your attack");
	}
	public override void OnDamage(Character attacker, int damage) {
		view.CreateFloatingMessage ("-" + damage.ToString(), Color.yellow);
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>은(는) " + damage + "의 피해를 입었습니다.");
	}
	public override void OnDestroy() {
		GameObject.Destroy (view.gameObject);
		view = null;
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color> 은(는) 죽었습니다.");
		LogView.Instance.Write ("You get " + reward.exp + " exp");
		LogView.Instance.Write ("You get " + reward.gold + " gold");
	}
}

