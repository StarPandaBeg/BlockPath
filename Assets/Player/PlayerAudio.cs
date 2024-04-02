using Blocks.PathPoints;
using Game.Audio;
using Game.Util;
using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private AudioManager _audioManager;

        public void Finish() {
            _audioManager.TryPlay("PlayerFinish");
            Vibration.Vibrate(30);
        }

        public void Die() {
            _audioManager.TryPlay("PlayerDie");
            Vibration.Vibrate(100);
        }
        
        private void OnEnable() {
            _playerMovement.OnBlockTouch += OnBlockTouch;
        }

        private void OnDisable() {
            _playerMovement.OnBlockTouch -= OnBlockTouch;
        }

        private void OnBlockTouch(PathPoint obj) {
            if (obj.Soft) return;
            // if (obj.ac)
            _audioManager.TryPlay("PlayerBlockHit");
        }
    }
}
