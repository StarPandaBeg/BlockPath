using System;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [RequireComponent(typeof(ButtonPathPoint))]
    public class ButtonPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _renderers;
        
        private ButtonPathPoint _point;

        private void Awake() {
            _point = GetComponent<ButtonPathPoint>();
        }

        private void OnEnable() {
            _point.StateChanged += StateChanged;
            StateChanged(_point.State);
        }
        
        private void OnDisable() {
            _point.StateChanged -= StateChanged;
        }

        private void StateChanged(bool[] state) {
            for (int i = 0; i < state.Length; i++) {
                _renderers[i].enabled = state[i];
            }
        }
    }
}