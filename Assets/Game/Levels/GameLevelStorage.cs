using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Tutorial;
using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(menuName = "Storage/Level Storage")]
    public class GameLevelStorage : ScriptableObject, IEnumerable<GameLevelStorage.LevelData>
    {
        [SerializeField] private List<LevelData> _levels;

        public IReadOnlyList<LevelData> Levels => _levels;
        public LevelData First => _levels.FirstOrDefault();
        public LevelData Last => _levels.LastOrDefault();

        public void AddLevel(GameLevelData levelData, string levelName) {
            _levels.Add(new LevelData()
            {
                Name = levelName, 
                Data = levelData
            });
        }
    
        public void AddLevel(GameLevelData levelData) {
            AddLevel(levelData, levelData.name);
        }

        public bool IsExists(string levelName) {
            var exists = _levels.FirstOrDefault(l => l.Name == levelName);
            return (exists.Data != null);
        }

        [Serializable]
        public struct LevelData
        {
            public string Name;
            public GameLevelData Data;
        }

        public IEnumerator<LevelData> GetEnumerator() {
            return _levels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}