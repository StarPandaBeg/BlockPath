using System;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Blocks.PathPoints
{
    public abstract class PathPoint : MonoBehaviour
    {
        private Vector2Int _tilePosition;
    
        public abstract PointType Type { get; }
        public LevelMap Map { get; set; }
        public virtual bool Soft => false;

        public Action<Vector2Int> TouchEvent;
        public Action InsideEvent;

        public Vector2Int TilePosition
        {
            get => _tilePosition;
            set
            {
                _tilePosition = value;
                transform.position = Map.Tilemap.GetCellCenterWorld(new Vector3Int(value.x, value.y));
            }
        }
    
        public enum PointType
        {
            Solid,
            Transparent,
            Freezer,
            Rotation
        }

        public void Touch(Vector2Int side) {
            if (!enabled) return;
            TouchEvent?.Invoke(side);
            OnPlayerTouch(side);
        }

        public void Inside() {
            if (!enabled) return;
            InsideEvent?.Invoke();
            OnPlayerInside();
        }
    
        protected virtual void OnPlayerTouch(Vector2Int side) {}
        protected virtual void OnPlayerInside() {}
    }
}
