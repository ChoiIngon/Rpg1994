using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MapEditor
{
    public class Menu : MonoBehaviour
    {
        [MenuItem("Assets/Create/Dungeon")]
        public static void CreateDungeon()
        {
            CreateAsset<Dungeon>("Assets/Dungeon.asset");
        }
        public static void CreateAsset<T>(string path) where T : ScriptableObject
        {
            T t = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(t, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = t;
        }
    }
}