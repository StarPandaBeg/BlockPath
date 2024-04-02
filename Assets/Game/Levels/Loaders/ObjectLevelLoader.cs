using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StarPanda.MapEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace Game.Levels.Loaders
{
    [CreateAssetMenu(menuName = "Save/Loaders/Object")]
    public class ObjectLevelLoader : LevelLoader
    {
        [SerializeField] private string _path = "Assets/Levels";
        [SerializeField] private GameLevelStorage _levelStorage;

        public void Reload() {
#if UNITY_EDITOR
            var found = AssetDatabase.FindAssets("t:GameLevel", new[] { _path });
            foreach (var uuid in found) {
                var path = AssetDatabase.GUIDToAssetPath(uuid);
                var obj = AssetDatabase.LoadAssetAtPath<GameLevelData>(path);
                
                var gameLevel = _levelStorage.Levels.FirstOrDefault((level) => level.Name == obj.name);
                if (gameLevel.Data == null) {
                    _levelStorage.AddLevel(obj);
                }
            }
#endif
        }

        public override async Task<T> Load<T>(string name) {
            var gameLevelEnumerable = _levelStorage.Levels.TakeWhile((level) => level.Name != name);
            var gameLevelIndex = gameLevelEnumerable.Count();
            if (gameLevelIndex == _levelStorage.Levels.Count) throw new FileNotFoundException("Level not found");

            var gameLevel = _levelStorage.Levels[gameLevelIndex];
            
            var builder = GameLevel.GetBuilder(gameLevel.Data.MapSize);
            foreach (var item in gameLevel.Data.Items) {
                builder.Set(item.TilePosition, item);
            }
            builder.SetIndex(gameLevelIndex);
            if (gameLevel.Data.Tutorial != null) builder.SetTutorial(gameLevel.Data.Tutorial);
            
            return (T)builder.Build();
        }

        public override async Task Save<T>(string name, T level) {
#if UNITY_EDITOR
            var path = Path.Combine(_path, $"{name}.asset");
            var obj = CreateInstance<GameLevelData>();
            var isNew = true;
            
            var found = AssetDatabase.FindAssets(name, new[] { _path });
            if (found.Length != 0) {
                path = AssetDatabase.GUIDToAssetPath(found[0]);
                obj = AssetDatabase.LoadAssetAtPath<GameLevelData>(path);
                isNew = false;
            }

            obj.name = name;
            obj.MapSize = level.MapSize;
            obj.Items.Clear();
            foreach (var data in level.Map) {
                if (data == null) continue;
                obj.Items.Add(data);
            }

            if (isNew) {
                _levelStorage.AddLevel(obj);
                AssetDatabase.CreateAsset(obj, path);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#else
            throw new System.NotImplementedException();
#endif
        }
    }
}