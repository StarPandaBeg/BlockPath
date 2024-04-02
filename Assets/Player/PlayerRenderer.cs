using System;
using Blocks.PathPoints;
using UnityEngine;

namespace Player
{
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Animator _spriteAnimator;
        [SerializeField] private ParticleSystem _dustParticleSystem;
        [Space]
        [SerializeField] private PlayerMovement _playerMovement;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsFinished = Animator.StringToHash("IsFinished");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public void Finish() {
            _spriteAnimator.SetBool(IsFinished, true);
        }
        
        public void Die() {
            _spriteAnimator.SetBool(IsDead, true);
        }

        private void OnEnable() {
            _playerMovement.OnMovementStep += OnMovementStep;
            _playerMovement.OnMovementStart += OnMovementStart;
            _playerMovement.OnMovementEnd += OnMovementEnd;
            _playerMovement.OnBlockTouch += OnBlockTouch;
        }

        private void OnDisable() {
            _playerMovement.OnMovementEnd -= OnMovementEnd;
            _playerMovement.OnMovementStart -= OnMovementStart;
            _playerMovement.OnMovementStep -= OnMovementStep;
            _playerMovement.OnBlockTouch -= OnBlockTouch;
        }

        private void OnBlockTouch(PathPoint point) {
            if (point.Soft) return;
            _dustParticleSystem.Play();
        }
        
        private void OnMovementStart(Vector2Int direction) {
            AdjustRotation(direction);
            _spriteAnimator.SetBool(IsMoving, true);
        }
        
        private void OnMovementEnd(Vector2Int obj) {
            _spriteAnimator.SetBool(IsMoving, false);
        }

        private void OnMovementStep(Vector2Int direction) {
            AdjustRotation(direction);
        }

        private void AdjustRotation(Vector2Int direction) {
            var angle = (float)Math.Atan2(direction.x, direction.y * -1) * Mathf.Rad2Deg;
            
            var rotation = _spriteTransform.rotation;
            rotation = Quaternion.Euler(rotation.x, rotation.y, angle);
            _spriteTransform.rotation = rotation;
        }
    }
}
