
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RotatablePathPoint))]
    public class RotatablePathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Transform _spriteTransform;
        
        private RotatablePathPoint _point;
        
        private void Awake() {
            _point = GetComponent<RotatablePathPoint>();
        }

        private void Start() {
            OnOrientationChange(_point.CurrentOrientation);
        }

        private void OnEnable() {
            _point.OrientationChange += OnOrientationChange;
        }

        private void OnDisable() {
            _point.OrientationChange -= OnOrientationChange;
        }

        private void OnOrientationChange(RotatablePathPoint.Orientation o) {
            Vector3 angle = Vector3.zero;

            if ((int)o > 1) angle.x = 180;
            if ((int)o % 2 != 0) angle.y = 180;
            
            _spriteTransform.rotation = Quaternion.Euler(angle);
        }
    }
}