using UI.Tutorial;
using UnityEngine;

namespace Game.Input.Nodes
{
    [CreateAssetMenu(menuName = "Input/Nodes/Blockable")]
    public class BlockableNode : InputNode
    {
        public bool Blocked;
        
        public override void Resolve(ref Vector2Int direction) {
            if (Blocked) {
                direction = Vector2Int.zero;
            }
        }
    }
}