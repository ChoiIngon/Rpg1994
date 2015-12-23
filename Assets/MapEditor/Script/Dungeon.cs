using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor
{
    public class Dungeon : ScriptableObject
    {
        public int width;
        public int height;
        public string id;
        public string name;
        public int roomCount;
    }
}