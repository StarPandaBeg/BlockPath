using StarPanda.MapEditor.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blocks
{
    [RequireComponent(typeof(Tilemap))]
    public class LevelMapDecorator : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [Space]
        [SerializeField] private bool _autoMode;
        [SerializeField] private LevelMap _map;
        [SerializeField] private Vector2Int _zoneSize;
        [Space]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _intensity = .5f;
        [Space]
        [SerializeField] private Tile[] _tiles;

        private Tilemap _tilemap;
        private Rect DecorationZone => new (_map.MapRect.center - _zoneSize / 2, _zoneSize);

        public void Clear() {
            _tilemap.ClearAllTiles();
        }

        public void Decorate(int seed = 0) {
            Clear();
            if (_tiles.Length == 0) return;
        
            var state = Random.state;
            Random.InitState(seed);
        
            var amount = (int)(_zoneSize.x * _zoneSize.y * _intensity * Random.Range(0.5f, 0.9f));
            for (int i = 0; i < amount; i++) {
                var x = Random.Range(0, _zoneSize.x * 2 - 1) - _zoneSize.x / 2;
                var y = Random.Range(0, _zoneSize.y * 2 - 1) - _zoneSize.y / 2;
                var tileIndex = Random.Range(0, _tiles.Length);

                _tilemap.SetTile(new Vector3Int(x, y, 0), _tiles[tileIndex]);
            }

            Random.state = state;
        }

        private void Awake() {
            _tilemap = GetComponent<Tilemap>();
        }

        private void OnEnable() {
            if (!_autoMode) return;
            _levelManager.LevelLoaded += OnLevelLoaded;
        }

        private void OnDisable() {
            if (!_autoMode) return;
            _levelManager.LevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(Level obj, string levelName) {
            Decorate(levelName.GetHashCode());
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(DecorationZone.center, DecorationZone.size);
        }
    }
}
