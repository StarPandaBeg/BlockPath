using System;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int AuthorShown = Animator.StringToHash("AuthorShown");
        private bool _isAuthorShown;

        public void ToggleAuthorPanel() {
            _animator.SetBool(AuthorShown, !_isAuthorShown);
            _isAuthorShown = !_isAuthorShown;
        }

        private void Awake() {
            _animator.SetBool(AuthorShown, false);
        }
    }
}
