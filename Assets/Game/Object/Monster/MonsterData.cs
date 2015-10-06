using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MonsterData : Character {
	public enum State
	{
		Idle,
		Chase,
		Fight
	}
	public int seq;
	public MonsterInfo info;
	public State state;
	public MonsterData() {
		category = Object.Category.Monster;
	}
	public bool CanSeePlayer() {
		Object.Position dest = Game.Instance.player.position;
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

	public override void Update() {
		if (true == CanSeePlayer ()) {
			if(range >= Vector2.Distance (position, Game.Instance.player.position)) {
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
		state = State.Idle;
		base.Move((Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max));
	}
	public virtual void Move() {
		state = State.Chase;
		base.Move((Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max));
	}
	public virtual void Attack() {
		state = State.Fight;
		OnAttack(Game.Instance.player, 0);
		if (true == Character.Hit (this, Game.Instance.player)) {
			int damage = GetAttack();
			int effectiveDamage = Math.Max (damage-Game.Instance.player.GetDefense(), 0);

			Game.Instance.player.SetDamage(this, effectiveDamage);
		}
	}
	public override void Destroy() {
		base.Destroy();
		if (null != items [(int)Character.EquipPart.Weapon]) {
			Object.Position at = new Object.Position(position.x, position.y);
			ItemStack itemStack = CreateItemStack (items [(int)Character.EquipPart.Weapon], at);
			items [(int)Character.EquipPart.Weapon] = null;
			//((MonsterView)view).OnDropItem (itemStack);
		}
		MonsterManager.Instance.Remove (seq);
	}
	public override void OnAttack(Character defender, int damage) {
		LogView.Button ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>", () => {
			InfoView.MonsterInfo(this);
		});
		LogView.Text (" 이(가) 당신을 공격합니다.\n");
	}
	public override void OnDamage(Character attacker, int damage) {
		LogView.Button ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>", () => {
			InfoView.MonsterInfo(this);
		});
		LogView.Text ("은(는) " + damage + "의 피해를 입었습니다.\n");
	}
	public override void OnDestroy() {
		LogView.Button ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>", () => {
			InfoView.MonsterInfo(this);
		});
		LogView.Text ("은(는) 죽었습니다.\n");
	}
}

