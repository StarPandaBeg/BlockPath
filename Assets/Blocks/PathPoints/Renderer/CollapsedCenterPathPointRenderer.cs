using System;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CollapsableCenterPathPoint))]
    public class CollapsedCenterPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Animator _animator;
        
        private CollapsableCenterPathPoint _point;
        private static readonly int IsCollapsed = Animator.StringToHash("IsCollapsed");

        private void Awake() {
            _point = GetComponent<CollapsableCenterPathPoint>();
        }

        private void Start() {
            OnOrientationChange(_point.CurrentOrientation);
        }

        private void OnEnable() {
            _point.CollapsedEvent += OnCollapse;
            _point.OrientationChange += OnOrientationChange;
        }

        private void OnDisable() {
            _point.CollapsedEvent -= OnCollapse;
            _point.OrientationChange -= OnOrientationChange;
        }
        
        private void OnOrientationChange(CollapsableCenterPathPoint.Orientation o) {
            Vector3 angle = Vector3.zero;
            if ((int)o > 0) angle.z = 90;
            _spriteTransform.rotation = Quaternion.Euler(angle);
        }

        private void OnCollapse() {
            _animator.SetBool(IsCollapsed, true);
        }
    }
}