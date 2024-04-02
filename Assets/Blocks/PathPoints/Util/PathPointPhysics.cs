using System;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Blocks.PathPoints.Util
{
    public class PathPointPhysics : MonoBehaviour
    {
        public bool TryMove(PathPoint current, PathPoint next, ref Vector2Int direction, ref Vector2Int position) {
            if (!CanMove(current, next, direction)) {
                return false;
            }
            PerformMove(current, next, ref direction, ref position);
            return true;
        }
    
        public bool CanMove(PathPoint current, PathPoint next, Vector2Int direction) {
            if (current is { Type: PathPoint.PointType.Freezer }) return false;
            if (current is { Type: PathPoint.PointType.Rotation }) return true;
            
            if (next is { Type: PathPoint.PointType.Rotation }) {
                return CanMoveToRotatable((RotatablePathPoint)next, direction);
            }
            
            if (next == null) return true;
            return next.Type != PathPoint.PointType.Solid;
        }

        public void PerformMove(PathPoint current, PathPoint next, ref Vector2Int direction, ref Vector2Int position) {
            if (!CanMove(current, next, direction)) throw new InvalidOperationException("Cannot move to the target");

            if (current is { Type: PathPoint.PointType.Rotation }) {
                ResolveRotation((RotatablePathPoint)current, ref direction);
            }
            position += direction;
        }
    
        private bool CanMoveToRotatable(RotatablePathPoint rotationPoint, Vector2Int direction) {
            var availableDirectionA = rotationPoint.SideA * -1;
            var availableDirectionB = rotationPoint.SideB * -1;
            if (direction != availableDirectionA && direction != availableDirectionB) return false;

            var rotatedDirection = direction;
            ResolveRotation(rotationPoint, ref rotatedDirection);
                
            var targetPoint = (rotationPoint.Map.Get(rotationPoint.TilePosition + rotatedDirection) as SaveEntry)
                ?.GetComponent<PathPoint>();
            if (targetPoint == null) return true;
            
            return targetPoint.Type != PathPoint.PointType.Solid;
        }

        private void ResolveRotation(RotatablePathPoint rotationPoint, ref Vector2Int direction) {
            var availableDirectionA = rotationPoint.SideA * -1;
            var availableDirectionB = rotationPoint.SideB * -1;

            if (direction == availableDirectionA) direction = rotationPoint.SideB;
            else if (direction == availableDirectionB) direction = rotationPoint.SideA;
        }
    }
}
