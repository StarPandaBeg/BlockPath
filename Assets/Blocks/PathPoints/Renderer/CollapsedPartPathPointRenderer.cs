using System;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CollapsablePartPathPoint))]
    public class CollapsedPartPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Animator _animator;
        
        private CollapsablePartPathPoint _point;
        private static readonly int IsCollapsed = Animator.StringToHash("IsCollapsed");

        private void Awake() {
            _point = GetComponent<CollapsablePartPathPoint>();
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
        
        private void OnOrientationChange(CollapsablePartPathPoint.Orientation o) {
            Vector3 angle = Vector3.zero;
            angle.z = -90 * (int)o;
            _spriteTransform.rotation = Quaternion.Euler(angle);
        }

        private void OnCollapse() {
            _animator.SetBool(IsCollapsed, true);
        }
    }
}