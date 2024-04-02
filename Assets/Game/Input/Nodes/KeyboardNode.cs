using UnityEngine;

namespace Game.Input.Nodes
{
    [CreateAssetMenu(menuName = "Input/Nodes/Keyboard")]
    public class KeyboardNode : InputNode
    {
        public override void Resolve(ref Vector2Int direction) {

            var x = UnityEngine.Input.GetAxisRaw("Horizontal") ;
            var y = UnityEngine.Input.GetAxisRaw("Vertical");

            if (x != 0) direction.x = (int)x;
            if (y != 0) direction.y = (int)y;

            if (x != 0 && y != 0) direction.y = 0;
        }
    }
}