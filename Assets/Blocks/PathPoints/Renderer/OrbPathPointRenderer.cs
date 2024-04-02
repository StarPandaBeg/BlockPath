using System;
using Game.Audio;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [RequireComponent(typeof(OrbPathPoint))]
    public class OrbPathPointRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioManager _audio;

        private OrbPathPoint _point;

        private void Awake() {
            _point = GetComponent<OrbPathPoint>();
        }

        private void OnEnable() {
            _point.Collected += OnCollected;
        }
        
        private void OnDisable() {
            _point.Collected -= OnCollected;
        }

        private void OnCollected() {
            _renderer.enabled = false;
            if (_audio != null) _audio.TryPlay("OrbCollected");
        }
    }
}