using System;

namespace Blocks.PathPoints
{
    public class TransparentPathPoint : PathPoint
    {
        public override PointType Type => _activated ? PointType.Solid : PointType.Transparent;
        public override bool Soft => !_activated;
        public Action Activated;

        private bool _activated;

        protected override void OnPlayerInside() {
            _activated = true;
            Activated?.Invoke();
        }
    }
}