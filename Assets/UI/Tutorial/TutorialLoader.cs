using System;
using Game.Input.Nodes;
using UnityEngine;

namespace UI.Tutorial
{
    public class TutorialLoader : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _tutorialSpawnPoint;
        [SerializeField] private BlockableNode _blockableNode;
        [Space]
        [SerializeField] private TutorialCanvas _tutorialCanvas;

        private bool _isShown;
        private TutorialPrefab _tutorialObj;

        public void Show(Tutorial t) {
            if (_tutorialObj != null) Destroy(_tutorialObj.gameObject);
            
            _tutorialObj = Instantiate(t.Prefab, _tutorialSpawnPoint);
            ResizeCameraToFit(_tutorialObj);

            _tutorialCanvas.HeaderText = t.Title.GetLocalizedString();
            _tutorialCanvas.SubheaderText = t.Subtitle.GetLocalizedString();
            _tutorialCanvas.MainText = t.Text.GetLocalizedString();
            _tutorialCanvas.Show();

            _isShown = true;
            _blockableNode.Blocked = true;
        }

        public void Hide() {
            if (!_isShown) return;
            _tutorialCanvas.Hide();
            _isShown = false;
            _blockableNode.Blocked = false;
        }

        private void ResizeCameraToFit(TutorialPrefab obj) {
            var aspectRatio = (float)_camera.targetTexture.width / _camera.targetTexture.height;
            var targetWidth = obj.Renderer.bounds.size.x;

            var targetHeight = targetWidth / aspectRatio;
            _camera.orthographicSize = targetHeight / 2;
        }

        private void Awake() {
            _blockableNode.Blocked = false;
        }

        private void OnEnable() {
            _tutorialCanvas.Close += Hide;
        }
        
        private void OnDisable() {
            _tutorialCanvas.Close -= Hide;
        }
    }
}