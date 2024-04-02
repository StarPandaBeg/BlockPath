using System;

namespace Blocks.PathPoints
{
    public class OrbPathPoint : PathPoint
    {
        public override PointType Type => PointType.Transparent;
        public override bool Soft => true;

        public Action Collected;

        protected override void OnPlayerInside() {
            Collected?.Invoke();
            enabled = false;
        }
    }
}