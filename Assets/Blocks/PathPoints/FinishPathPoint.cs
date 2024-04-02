using System;
using Game.Levels.Finish;
using UnityEngine;

namespace Blocks.PathPoints
{
    [RequireComponent(typeof(FinishPoint))]
    public class FinishPathPoint : PathPoint
    {
        public override PointType Type => _finishPoint.Activated ? PointType.Freezer : PointType.Solid;
        public override bool Soft => true;

        private FinishPoint _finishPoint;

        private void Awake() {
            _finishPoint = GetComponent<FinishPoint>();
        }

        protected override void OnPlayerInside() {
            _finishPoint.Finish();
        }
    }
}
