using System;
using System.Collections.Generic;
using Game.Audio;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Finish
{
    public class FinishManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [Space]
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private FinishCondition[] _conditions;

        public Action Finish;

        private readonly List<FinishPoint> _finishPoints = new();
        private LevelMap _map;
        private bool _shouldCheckConditions;
        private bool _canFinish;
        private bool _initialized;

        public void RefreshConditions() {
            _shouldCheckConditions = true;
        }

        private void OnEnable() {
            _levelManager.LevelLoaded += OnLevelLoaded;
        }
        
        private void OnDisable() {
            _levelManager.LevelLoaded -= OnLevelLoaded;
        }

        private void Update() {
            if (_shouldCheckConditions) {
                CheckConditions();
                _shouldCheckConditions = false;
            }
        }

        private void OnDestroy() {
            UnregisterFinishPoints();
            UnregisterConditions();
        }

        private void CheckConditions() {
            bool canFinish = true;
            foreach (var condition in _conditions) {
                if (!condition.Check()) {
                    canFinish = false;
                    break;
                }
            }

            if (!canFinish) {
                _initialized = true;
                DeactivatePoints();
                return;
            }
            
            if (_finishPoints.Count > 0) ActivatePoints();
            else Finish?.Invoke();
            
            _initialized = true;
        }

        private void OnLevelLoaded(Level level, string levelName) {
            _map = FindGameMap();
            _initialized = false;
            
            UnregisterFinishPoints();
            UnregisterConditions();
            
            RegisterFinishPoints();
            RegisterConditions();
            
            _shouldCheckConditions = true;
        }

        private void RegisterFinishPoints() {
            var points = _map.GetComponentsInChildren<FinishPoint>();
            _finishPoints.AddRange(points);
            _finishPoints.ForEach(point => point.FinishEvent += OnFinishTouched);
            DeactivatePoints();
        }
        
        private void UnregisterFinishPoints() {
            _finishPoints.ForEach(point => point.FinishEvent -= OnFinishTouched);
            _finishPoints.Clear();
        }

        private void RegisterConditions() {
            foreach (var condition in _conditions) {
                condition.InitCondition(this, _map);
            }
        }

        private void UnregisterConditions() {
            foreach (var condition in _conditions) {
                condition.DestroyCondition(this, _map);
            }
        }
        
        private void ActivatePoints() {
            if (_canFinish) return;
            
            _canFinish = true;
            _finishPoints.ForEach((point) => point.Activate());
            
            if (_audioManager != null && _initialized) _audioManager.TryPlay("PortalOpen");
        }

        private void DeactivatePoints() {
            _canFinish = false;
            _finishPoints.ForEach((point) => point.Deactivate());
        }

        private void OnFinishTouched() {
            if (!_canFinish) return;
            Finish?.Invoke();
        }
        
        private LevelMap FindGameMap() {
            var mapObject = GameObject.FindGameObjectWithTag("Game Map");
            if (mapObject == null) {
                throw new ApplicationException(
                    "The `Map` object couldn't be found on the scene. Ensure that you created Map properly and give it `Game Map` tag");
            }

            var map = mapObject.GetComponent<LevelMap>();
            if (map == null) {
                throw new ApplicationException(
                    "The `Map` object doesn't have a Map component. Ensure that you created Map properly.");
            }
            return map;
        }
    }
}