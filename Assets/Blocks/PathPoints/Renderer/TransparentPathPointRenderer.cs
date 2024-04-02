using System;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [RequireComponent(typeof(TransparentPathPoint))]
    public class TransparentPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private TransparentPathPoint _point;
        private static readonly int IsActive = Animator.StringToHash("IsActive");

        private void Awake() {
            _point = GetComponent<TransparentPathPoint>();
        }

        private void OnEnable() {
            _point.Activated += OnActivated;
        }
        
        private void OnDisable() {
            _point.Activated -= OnActivated;
        }

        private void OnActivated() {
            _animator.SetBool(IsActive, true);
        }
    }
}