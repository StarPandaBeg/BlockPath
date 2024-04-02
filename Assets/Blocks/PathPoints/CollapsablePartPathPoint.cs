using System;
using UnityEngine;

namespace Blocks.PathPoints
{
    public class CollapsablePartPathPoint : PathPoint
    {
        [SerializeField] private Orientation _orientation;
        
        public override PointType Type => Collapsed ? PointType.Transparent : PointType.Solid;
        public override bool Soft => Collapsed;
        
        public CollapsableCenterPathPoint Center { get; set; }
        public bool Collapsed => Center.Collapsed;
        public Orientation CurrentOrientation => _orientation;
        public Action CollapsedEvent;
        public Action<Orientation> OrientationChange;
        
        public enum Orientation
        {
            W,
            N,
            E,
            S
        }

        public void Collapse() {
            CollapsedEvent?.Invoke();
        }
        
        public void SetOrientation(Orientation o) {
            if (o == _orientation) return;
            
            _orientation = o;
            OrientationChange?.Invoke(o);
        }
    }
}