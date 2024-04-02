
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingScreen : MonoBehaviour
{
    private Animator _animator;
    private bool _ready;
    private bool _gone;
    
    private static readonly int TriggerStart = Animator.StringToHash("Start");
    private static readonly int TriggerEnd = Animator.StringToHash("End");

    public IEnumerator StartLoading() {
        if (_ready) yield break;

        _gone = false;
        _animator.SetTrigger(TriggerStart);
        
        while (!_ready) yield return null;
        _ready = true;
    }
    
    public IEnumerator EndLoading() {
        if (_gone) yield break;
        
        _ready = false;
        _animator.SetTrigger(TriggerEnd);
        
        while (!_gone) yield return null;
        _gone = true;
    }

    public void OnLoadingScreenReady() {
        _ready = true;
    }
    
    public void OnLoadingScreenGone() {
        _gone = true;
    }


    private void Awake() {
        _animator = GetComponent<Animator>();
    }
}
