using StarPanda.MapEditor.Runtime;
using UI.Tutorial;
using UnityEngine;

namespace Game.Levels
{
    public class GameLevel : Level
    {
        public int Index => _index;
        private int _index;

        public bool HasTutorial => _tutorial.Prefab != null;
        public Tutorial Tutorial => _tutorial;
        private Tutorial _tutorial;

        public new static GameLevelBuilder GetBuilder(Vector2Int size) {
            return new GameLevelBuilder(size);
        }
        
        public new static GameLevelBuilder GetBuilder(int x, int y) {
            return new GameLevelBuilder(x, y);
        }
        
        public class GameLevelBuilder : LevelBuilder
        {
            private int _index = -1;
            private Tutorial _tutorial;
            
            public GameLevelBuilder(Vector2Int size) : base(size) { }
            public GameLevelBuilder(int x, int y) : base(x, y) { }

            public GameLevelBuilder SetIndex(int index) {
                _index = index;
                return this;
            }
            
            public GameLevelBuilder SetTutorial(Tutorial t) {
                _tutorial = t;
                return this;
            }

            public override Level Build() {
                var level = Build<GameLevel>();
                level._index = _index;
                level._tutorial = _tutorial;
                return level;
            }
        }
    }
}