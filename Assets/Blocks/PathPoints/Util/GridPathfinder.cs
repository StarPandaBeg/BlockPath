using System.Collections.Generic;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Blocks.PathPoints.Util
{
    [RequireComponent(typeof(PathPointPhysics))]
    public class GridPathfinder : MonoBehaviour
    {
        [SerializeField] private LevelMap _map;
        private PathPointPhysics _physics;

        private void Awake() {
            _physics = GetComponent<PathPointPhysics>();
        }

        public List<PathEntry> FindPath(Vector2Int startCellPosition, Vector2Int movementDirection) {
            var path = new List<PathEntry>();
            var data = new PathData(startCellPosition, movementDirection);
        
            while (data.CanMove) {
                path.Add(new PathEntry(data.Point, data.Position));
                MovementStep(ref data, data.Position == startCellPosition);
            }
        
            return path;
        }

        private void MovementStep(ref PathData data, bool isStartPoint) {
            var currentPoint = isStartPoint ? null : (_map.Get(data.Position) as SaveEntry)?.GetComponent<PathPoint>();
            var nextPoint = (_map.Get(data.NewPosition) as SaveEntry)?.GetComponent<PathPoint>();
        
            data.CanMove = _physics.TryMove(currentPoint, nextPoint, ref data.Direction, ref data.Position);
            data.Point = (_map.Get(data.Position) as SaveEntry)?.GetComponent<PathPoint>();
        
            if (!_map.IsInside(data.Position)) data.CanMove = false;
        }
    
        public struct PathEntry
        {
            public readonly PathPoint Point;
            public readonly Vector2Int Position;

            public PathEntry(PathPoint point, Vector2Int position) {
                Point = point;
                Position = position;
            }
        }

        private class PathData
        {
            public PathPoint Point;
            public Vector2Int Position;
            public Vector2Int Direction;
            public bool CanMove = true;

            public Vector2Int NewPosition => Position + Direction;
        
            public PathData(Vector2Int position, Vector2Int direction) {
                Position = position;
                Direction = direction;
            }
        }
    }
}
