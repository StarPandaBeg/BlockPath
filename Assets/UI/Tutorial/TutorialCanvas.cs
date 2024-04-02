using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace UI.Tutorial
{
    public class TutorialCanvas : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [Space]
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _subheaderText;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private Button _closeButton;
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");
        
        public Action Close;

        public string HeaderText
        {
            get => _headerText.text;
            set => _headerText.text = value;
        }
        
        public string SubheaderText
        {
            get => _subheaderText.text;
            set => _subheaderText.text = value;
        }
        
        public string MainText
        {
            get => _mainText.text;
            set => _mainText.text = value;
        }

        public void Show() {
            _animator.SetBool(IsActive, true);
        }

        public void Hide() {
            _animator.SetBool(IsActive, false);
        }

        private void OnEnable() {
            _closeButton.onClick.AddListener(OnTryClose);
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveListener(OnTryClose);
        }

        private void Update() {
            if (Input.GetAxis("Submit") > 0) {
                OnTryClose();
            }
        }

        private void OnTryClose() {
            Close?.Invoke();
        }
    }
}