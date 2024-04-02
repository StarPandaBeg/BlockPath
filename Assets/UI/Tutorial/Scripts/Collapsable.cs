using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsable : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private static readonly int IsCollapsed = Animator.StringToHash("IsCollapsed");

    public void Collapse() {
        _animator.SetBool(IsCollapsed, true);
    }
    
    public void Open() {
        _animator.SetBool(IsCollapsed, false);
    }
}
