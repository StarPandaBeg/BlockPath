using UnityEngine;

namespace Game.Input.Nodes
{
    [CreateAssetMenu(menuName = "Input/Nodes/Swipe")]
    public class SwipeNode : InputNode
    {
        public override void Resolve(ref Vector2Int direction) {
            if (SwipeDetector.DetectSwipe(out var targetDirection)) {
                direction = targetDirection;
            }
        }
    }
}