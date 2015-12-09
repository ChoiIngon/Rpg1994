using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestStartCondition
{
	public abstract bool IsAvailable();
}

public class QuestStartCondition_Level : QuestStartCondition
{
	public override bool IsAvailable() {
		return true;
	}
}

public class QuestStartCondition_Complete : QuestStartCondition
{
	public List<string> completeQuestIDs = new List<string> ();
	
	public override bool IsAvailable() {
		foreach (string questID in completeQuestIDs) {
			if (false == QuestManager.Instance.complete.ContainsKey (questID)) {
				return false;
			}
		}
		return true;
	}
}

public class QuestStartCondition_Incomplete : QuestStartCondition {
	public List<string> incompleteQuestIDs = new List<string> ();
	public override bool IsAvailable() {
		foreach (string questID in incompleteQuestIDs) {
			if (true == QuestManager.Instance.complete.ContainsKey (questID)) {
				return false;
			}
		}
		return true;
	}
}
