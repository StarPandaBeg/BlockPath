using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Loaders
{
    [CreateAssetMenu(menuName = "Save/Loaders/Memory")]
    public class MemoryLevelLoader : LevelLoader
    {
        private readonly int[][] _map =
        {
            new[] {0, 0, 0, 0, 0, 0, 0, 0},
            new[] {0, 0, 2, 0, 0, 0, 0, 0},
            new[] {2, 0, 0, 0, 0, 0, 0, 0},
            new[] {0, 0, 0, 0, 1, 0, 2, 0},
            new[] {0, 0, 0, 4, 0, 0, 0, 0},
            new[] {0, 2, 0, 0, 0, 0, 0, 0},
            new[] {0, 0, 0, 0, 0, 2, 0, 0},
            new[] {0, 0, 0, 0, 0, 0, 0, 0},
        };

        private readonly Dictionary<int, string> _ids = new ()
        {
            { 1, "player" },
            { 2, "solid" },
            { 4, "finish" }
        };

        public override async Task<T> Load<T>(string name) {
            var builder = GameLevel.GetBuilder(8, 8);

            for (int row = 0; row < _map.Length; row++) {
                for (int column = 0; column < _map[row].Length; column++) {
                    var position = new Vector2Int(column, row);
                    var item = _map[row][column];

                    if (_ids.TryGetValue(item, out var id)) {
                        builder.Set(position, new SaveData() {ID = id, TilePosition = position});
                    }
                }
            }
        
            return (T)builder.Build();
        }

        public override Task Save<T>(string name, T level) {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Save file name: {name}");
            for (int row = 0; row < level.MapSize.y; row++) {
                for (int column = 0; column < level.MapSize.x; column++) {
                    var position = new Vector2Int(column, row);
                    var data = level.Get(position);
                    if (data == null) continue;
                    builder.AppendLine($"{position} - {data.ID}");
                }
            }

            Debug.Log(builder.ToString());
            return Task.CompletedTask;
        }
    }
}