using System;
using UnityEngine;

namespace Blocks.PathPoints
{
    public class RotatablePathPoint : PathPoint
    {
        [SerializeField] private Orientation _orientation;
        
        public override PointType Type => PointType.Rotation;
        
        public Orientation CurrentOrientation => _orientation;
        public Vector2Int SideA => GetXSideNormal(_orientation);
        public Vector2Int SideB => GetYSideNormal(_orientation);
        public Action<Orientation> OrientationChange;

        public enum Orientation
        {
            NW,
            NE,
            SW,
            SE
        }

        public void SetOrientation(Orientation o) {
            if (o == _orientation) return;
            
            _orientation = o;
            OrientationChange?.Invoke(o);
        }

        private Vector2Int GetXSideNormal(Orientation o) {
            if ((int)o < 2) return Vector2Int.up;
            return Vector2Int.down;
        }
        
        private Vector2Int GetYSideNormal(Orientation o) {
            if ((int)o % 2 == 0) return Vector2Int.left;
            return Vector2Int.right;
        }
    }
}
