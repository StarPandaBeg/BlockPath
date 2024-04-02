using System;
using UnityEngine;

namespace Blocks.PathPoints
{
    public class ButtonPathPoint : PathPoint
    {
        public override PointType Type => PointType.Solid;
        public bool[] State => _state;
        private readonly bool[] _state = new bool[4];

        public Action<bool[]> StateChanged;

        protected override void OnPlayerTouch(Vector2Int side) {
            int index = GetSideIndex(side);
            if (_state[index]) return;

            _state[index] = true;
            StateChanged?.Invoke(_state);
        }
        
        public int GetSideIndex(Vector2Int side) {
            if (side.magnitude > 1) {
                throw new ArgumentOutOfRangeException(nameof(side), "Invalid side normal vector");
            }
            return (side.y == 0) ? (side.x == 1 ? 2 : 3) : (side.y == 1 ? 0 : 1);
        }
    }
}