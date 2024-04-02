using System;
using Game.Levels.Finish;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [RequireComponent(typeof(FinishPathPoint), typeof(FinishPoint))]
    public class FinishPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private FinishPoint _finishPoint;
        
        private static readonly int IsActive = Animator.StringToHash("IsActive");
        
        private void Awake() {
            _finishPoint = GetComponent<FinishPoint>();
        }

        private void OnEnable() {
            _finishPoint.OnStateChange += OnStateChange;
        }
        
        private void OnDisable() {
            _finishPoint.OnStateChange -= OnStateChange;
        }

        private void OnStateChange(bool state) {
            _animator.SetBool(IsActive, state);
        }
    }
}