using System;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Util
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private LevelMap _map;
        [SerializeField] private float _scale = 1.0f;

        private Camera _camera;

        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void OnEnable() {
            _levelManager.LevelLoaded += OnLevelLoaded;
        }

        private void OnDisable() {
            _levelManager.LevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(Level level, string levelName) {
            var rect = _map.MapRect;

            var aspectRatio = (float)Screen.width / Screen.height;
            var targetHeight = rect.width / aspectRatio;
        
            transform.position = new Vector3(rect.center.x, rect.center.y);
            _camera.orthographicSize = Math.Max(targetHeight / 2, rect.height / 2) * _scale;
        }
    }
}
