using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MapEditor {
	[CustomEditor(typeof(MapEditor.Map))]
	public class MapInspector : Editor {
		public string path;

		public override void OnInspectorGUI() {
			DrawDefaultInspector ();

			MapEditor.Map map = (MapEditor.Map)target;
			if (null == map) {
				return;
			}
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Description : ", "");
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			map.mapDescription = EditorGUILayout.TextArea (map.mapDescription, GUILayout.Height (70));
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal (); 
			EditorGUILayout.LabelField ("file", path);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal (); 

			if (GUILayout.Button ("save")) {
				/*
				path = EditorUtility.SaveFilePanel ("Save map file as json", "", "RegionInfo.json", "json");
				if (0 == path.Length) {
					throw new System.Exception ("no file name");
				}
				map.Save (path);
				*/
				map.Save ("/Users/kukuta/workspace/Rpg1994/RegionInfo.json");
			}
			if (GUILayout.Button ("load")) {
				/*
				path = EditorUtility.OpenFilePanel ("Open map file as json", "", "json");
				if(0==path.Length)
				{
					throw new System.Exception("no file name");
				}
				map.Load (path);
				*/
				map.Load ("/Users/kukuta/workspace/Rpg1994/RegionInfo.json");
			}
			EditorGUILayout.EndHorizontal ();
			
			//base.OnInspectorGUI ();
			if (true == GUI.changed) {
				EditorUtility.SetDirty (target);
			}
		}
	}
}