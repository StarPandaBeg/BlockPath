using System;
using Blocks.PathPoints.Save;
using UnityEngine;

namespace Blocks.PathPoints
{
    public class CollapsableCenterPathPoint : PathPoint
    {
        [SerializeField] private Orientation _orientation;
        
        public override PointType Type => _collapsed ? PointType.Solid : PointType.Transparent;
        public override bool Soft => !_collapsed;
        public bool Collapsed => _collapsed;
        public Orientation CurrentOrientation => _orientation;
        
        public Action CollapsedEvent;
        public Action<Orientation> OrientationChange;
        
        private bool _collapsed;
        private CollapsablePartPathPoint[] _parts;
        private bool _needToSetup = true;

        public enum Orientation
        {
            WE,
            NS
        }
        
        public void SetOrientation(Orientation o) {
            if (o == _orientation) return;
            
            _orientation = o;
            _needToSetup = true;
            
            OrientationChange?.Invoke(o);
        }

        protected override void OnPlayerInside() {
            _collapsed = true;
            CollapsedEvent?.Invoke();
            
            _parts[0].Collapse();
            _parts[1].Collapse();
        }

        private void Update() {
            if (_needToSetup) SetupParts();
        }

        private CollapsablePartPathPoint[] GetParts() {
            var parts = new CollapsablePartPathPoint[2];
            Vector2Int posA = Vector2Int.one * -1;
            Vector2Int posB = Vector2Int.one * -1;

            if (_orientation == Orientation.NS) {
                posA = TilePosition + Vector2Int.up;
                posB = TilePosition + Vector2Int.down;
            }
            else if (_orientation == Orientation.WE) {
                posA = TilePosition + Vector2Int.left;
                posB = TilePosition + Vector2Int.right;
            }
            
            parts[0] = (CollapsablePartPathPoint)((PathPointSaveEntry)Map.Get(posA))?.Point;
            parts[1] = (CollapsablePartPathPoint)((PathPointSaveEntry)Map.Get(posB))?.Point;
            
            if (parts[0] == null || parts[1] == null) { 
                throw new InvalidOperationException("Collapsable point is not valid");
            }
            
            return parts;
        }

        private void SetupParts() {
            _parts = GetParts();
            _parts[0].Center = _parts[1].Center = this;
            _needToSetup = false;
        }
    }
}