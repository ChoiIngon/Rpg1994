using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {
	public class Door {
		public bool locked;
		public bool opened;

		public enum DirectionType {
			Vertical, Horizon
		};

		public Position position = new Position();
		public DirectionType direction = DirectionType.Vertical;
	}
}