using System.Collections;
using System.Collections.Generic;
using Game.Input.Nodes;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/Flow")]
public class InputFlow : ScriptableObject
{
    [SerializeField] private InputNode[] _nodes;
    
    public bool Get(out Vector2Int direction) {
        direction = Vector2Int.zero;
        foreach (var node in _nodes) {
            node.Resolve(ref direction);
        }
        return direction != Vector2Int.zero;
    }
}
