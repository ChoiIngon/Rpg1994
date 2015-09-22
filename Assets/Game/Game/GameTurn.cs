using UnityEngine;
using System;
using System.Collections;

public abstract class ITurnCounter
{
	public abstract int GetTurn ();
	public abstract void NextTurn ();
};

public class TurnCounter : ITurnCounter {
	public int turn = 1;
	public override int GetTurn() {
		return turn;
	}
	public override void NextTurn()	{
		turn++;
	}
};

public class UnixTimestamp : ITurnCounter {
	public int turn = 0;
	public UnixTimestamp() {
		NextTurn();
	}
	public override int GetTurn() {
		return turn;
	}
	public override void NextTurn() {
		turn = (int)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
	}
};

public class GameTurn {
	ITurnCounter impl = null;
	public void Init<T> () where T : ITurnCounter, new() 
	{
		impl = new T ();
	}
	public int GetTurn() {
		return impl.GetTurn();
	}

	public void NextTurn() {
		impl.NextTurn ();
	}
}
