using System.Collections.Generic;
using StarPanda.MapEditor.Runtime;
using UI.Tutorial;
using UnityEngine;

namespace Game.Levels
{
    public class GameLevelData : ScriptableObject
    {
        public Vector2Int MapSize;
        public List<SaveData> Items = new ();
        [Space]
        public bool HasTutorial;
        public Tutorial Tutorial;
    }
}