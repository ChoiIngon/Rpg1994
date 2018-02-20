using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public abstract class QuestStartCondition
{
	public abstract bool IsAvailable();
}

public class QuestStartCondition_MeetNpc : QuestStartCondition {
	public string npcID;
	public override bool IsAvailable() {
		/*
		Object target = Player.Instance.target;
		if (null != target && Object.Category.NPC == target.category) {
			Npc npc = (Npc)target;
			if(npc.id == npcID)
			{
				return true;
			}
		}
		*/
		return false;
	}
}

public class QuestStartCondition_Level : QuestStartCondition
{
	public int level;
	public override bool IsAvailable() {
		/*
		if (level <= Player.Instance.level) {
			return true;
		}
		*/
		return false;
	}
}

public class QuestStartCondition_Complete : QuestStartCondition {
	public string questID;
	public override bool IsAvailable() {
		if (false == QuestManager.Instance.completes.ContainsKey (questID)) {
			return false;
		}
		return true;
	}
}

public class QuestStartCondition_Incomplete : QuestStartCondition {
	public string questID;
	public override bool IsAvailable() {
		if (true == QuestManager.Instance.completes.ContainsKey (questID)) {
			return false;
		}
		return true;
	}
}
