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

	public class RewardData {
		int gold;
		List<ItemStack> itemStacks = new List<ItemStack>(); 
	};

	public int seq;
	public MonsterInfo info;
	public State state;
	public ObjectView view;
	RewardData rewared;

	public MonsterData() {
		category = Object.Category.Monster;
	}
	public bool CanSeePlayer() {
		Object.Position dest = GameManager.Instance.player.position;
		if(sight < Vector2.Distance (new Vector2 (this.position.x, this.position.y), new Vector2 (dest.x, dest.y))) {
			return false;
		}
		
		List<Object.Position> positions = Raycast(new Object.Position(dest.x, dest.y));
		foreach(Object.Position position in positions) {
			if(position == dest) {
				return true;
			}
			Tile tile = GameManager.Instance.map.tiles[position.x + position.y * GameManager.Instance.map.width];
			if(Tile.Type.Floor != tile.type)
			{
				return false;
			}
		}
		return false;
	}

	public override void Update() {
		if (true == CanSeePlayer ()) {
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
		Move();
		state = State.Idle;
	}
	public virtual void Move() {
		base.Move((Character.DirectionType)UnityEngine.Random.Range(0, (int)Character.DirectionType.Max));
		if (null != view) {
			view.SetVisible (visible);
		}
		state = State.Chase;
	}
	public virtual void Attack() {
		state = State.Fight;
		OnAttack(GameManager.Instance.player, 0);
		if (true == Character.Hit (this, GameManager.Instance.player)) {
			int damage = GetStatus().attack;
			int effectiveDamage = Math.Max (damage-GameManager.Instance.player.GetStatus().defense, 0);

			GameManager.Instance.player.SetDamage(this, effectiveDamage);
		}
	}
	public override void Destroy() {
		state = State.Die;
		base.Destroy();
		if (null != items [(int)Character.EquipPart.Weapon]) {
			Object.Position at = new Object.Position(position.x, position.y);
			ItemStack itemStack = CreateItemStack (items [(int)Character.EquipPart.Weapon], at);
			items [(int)Character.EquipPart.Weapon] = null;
		}
		MonsterManager.Instance.Remove (seq);
	}

	public override void OnCreate() {
		view = ObjectView.Create<ObjectView> (this, "M", Color.red);
		view.position = position;
		view.transform.SetParent (MapView.Instance.tiles, false);
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnMove(Character.DirectionType direction)
	{
		view.position = position;
		view.transform.localPosition = new Vector3(position.x * MapView.TILE_SIZE, -position.y * MapView.TILE_SIZE, 0);
	}

	public override void OnAttack(Character defender, int damage) {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>이(가) 당신을 공격합니다.");
	}
	public override void OnDamage(Character attacker, int damage) {
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color>은(는) " + damage + "의 피해를 입었습니다.");
	}
	public override void OnDestroy() {
		GameObject.Destroy (view.gameObject);
		LogView.Instance.Write ("<color=red>" + name + "[" + position.x + "," + position.y + "]</color> 은(는) 죽었습니다.");
	}
}

