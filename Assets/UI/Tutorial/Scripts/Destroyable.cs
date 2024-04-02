using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.Tutorial.Scripts
{
    public class Destroyable : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _label;
        
        private static readonly int IsDestroyed = Animator.StringToHash("IsDestroyed");
        private static readonly int Timer = Animator.StringToHash("Timer");

        public void Run() {
            StopAllCoroutines();
        
            _animator.SetInteger(Timer, 0);
            _animator.SetBool(IsDestroyed, false);
            _label.text = String.Empty;
        
            StartCoroutine(RunTimer());
        }

        private IEnumerator RunTimer() {
            for (int i = 9; i > 0; i--) {
                _animator.SetInteger(Timer, i);
                _label.text = i.ToString();
                yield return new WaitForSeconds(0.1f);
            }
            
            _animator.SetInteger(Timer, 0);
            _animator.SetBool(IsDestroyed, true);
            _label.text = String.Empty;
        }
    }
}
