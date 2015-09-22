using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MapEditor {
	[CustomEditor(typeof(MapEditor.Map))]
	public class MapInspector : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector ();

			MapEditor.Map map = (MapEditor.Map)target;
			if (GUILayout.Button ("save")) {
				string path = EditorUtility.SaveFilePanel("Save map file as json", "", "RegionInfo.json",  "json");
				if(0==path.Length)
				{
					throw new System.Exception("no file name");
				}
				Debug.Log (path);
				map.Save(path);
			}
			if (GUILayout.Button ("load")) {
				string path = EditorUtility.OpenFilePanel ("Open map file as json", "", "json");
				if(0==path.Length)
				{
					throw new System.Exception("no file name");
				}
				map.Load (path);
				//map.Load(path);
			}
		}
	}
}