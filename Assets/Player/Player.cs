using System;
using Blocks.PathPoints;
using Game.Settings;
using Game.Util;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameSettings _settings;
        [Space]
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerRenderer _renderer;
        [SerializeField] private PlayerAudio _audio;

        public PlayerMovement Physics => _movement;
        public PlayerRenderer Renderer => _renderer;
        public PlayerAudio Audio => _audio;

        private void OnEnable() {
            _movement.OnBlockTouch += OnBlockTouch;
        }

        private void OnDisable() {
            _movement.OnBlockTouch -= OnBlockTouch;
        }

        private void OnBlockTouch(PathPoint obj) {
            if (obj.Soft) return;
            if (!_settings.VibroEnabled) return;
            Vibration.Vibrate(10);
        }

        public void Finish() {
            Physics.Finish();
            Renderer.Finish();
            Audio.Finish();
        }
        
        public void Die() {
            Physics.Die();
            Renderer.Die();
            Audio.Die();
        }
    }
}