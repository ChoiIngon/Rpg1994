using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterData : Character {
	public int seq;
	public MonsterInfo info;

	public bool IsVisible(Object.Position dest) {
		if(sight < Vector2.Distance (new Vector2 (this.position.x, this.position.y), new Vector2 (dest.x, dest.y))) {
			return false;
		}
		
		List<Object.Position> positions = Raycast(new Object.Position(dest.x, dest.y));
		foreach(Object.Position position in positions) {
			if(position == dest) {
				return true;
			}
			Tile tile = Game.Instance.map.tiles[position.x + position.y * Game.Instance.map.width];
			if(Tile.Type.Floor != tile.type)
			{
				return false;
			}
		}
		return false;
	}

	public override void Action() {
		if (true == IsVisible (Game.Instance.player.position)) {
			if(GetAttackRange() >= Vector2.Distance (position, Game.Instance.player.position)) {
				Attack ();
				return;
			}
			else {
				Move ();
				return;
			}
		}
		Idle ();
		GetState ();
	}
	public virtual void Idle() {
		base.Move((Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max));
	}
	public virtual void Move() {
		base.Move((Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max));
	}
	public virtual void Attack() {
		MonsterView view = (MonsterView)this.view;
		if (true == Character.Hit (this, Game.Instance.player)) {
			int damage = GetAttack();
			int effectiveDamage = Math.Max (damage-Game.Instance.player.GetDefense(), 0);
			view.OnAttack(Game.Instance.player, effectiveDamage);
			Game.Instance.player.SetDamage(effectiveDamage);
		}
		else {
			view.OnAttack(Game.Instance.player, 0);
		}
	}
	public override void Destroy() {
		if (null != view) {
			((MonsterView)this.view).Destroy ();
		}
		MonsterManager.Instance.Remove (seq);
	}
}

