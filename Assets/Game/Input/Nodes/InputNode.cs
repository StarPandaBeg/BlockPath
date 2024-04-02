using UnityEngine;

namespace Game.Input.Nodes
{
    public abstract class InputNode : ScriptableObject
    {
        public abstract void Resolve(ref Vector2Int direction);
    }
}