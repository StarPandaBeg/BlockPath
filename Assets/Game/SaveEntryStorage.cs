using System.Collections.Generic;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Storage/Save Entry Storage")]
    public class SaveEntryStorage : SaveStorage
    {
        [SerializeField] private SaveEntry[] _entries;

        private readonly Dictionary<string, SaveEntry> _entriesDict = new();
    
        private void OnEnable() {
            _entriesDict.Clear();
        
            foreach (var node in _entries) {
                _entriesDict.Add(node.ID, node);
            }
        }

        public override ISaveEntry ResolvePrefab(SaveData data) {
            if (!_entriesDict.ContainsKey(data.ID)) return null;
            return _entriesDict[data.ID];
        }
    }
}
