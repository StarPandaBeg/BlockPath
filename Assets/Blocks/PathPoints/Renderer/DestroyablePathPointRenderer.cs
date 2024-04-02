using System;
using TMPro;
using UnityEngine;

namespace Blocks.PathPoints.Renderer
{
    [RequireComponent(typeof(DestroyablePathPoint))]
    public class DestroyablePathPointRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _label;
        
        private static readonly int IsDestroyed = Animator.StringToHash("IsDestroyed");
        private static readonly int Timer = Animator.StringToHash("Timer");

        private DestroyablePathPoint _point;

        private void Awake() {
            _point = GetComponent<DestroyablePathPoint>();
        }

        private void OnEnable() {
            _point.TimerStarted += OnTimerStarted;
            _point.TimerStep += OnTimerStep;
            _point.TimerEnded += OnTimerEnded;
        }
        
        private void OnDisable() {
            _point.TimerStarted -= OnTimerStarted;
            _point.TimerStep -= OnTimerStep;
            _point.TimerEnded -= OnTimerEnded;
        }

        private void OnTimerStarted(int value) {
            _animator.SetInteger(Timer, value);
            _label.text = value.ToString();
        }

        private void OnTimerStep(int value) {
            _animator.SetInteger(Timer, value);
            _label.text = value.ToString();
        }

        private void OnTimerEnded() {
            _animator.SetInteger(Timer, 0);
            _animator.SetBool(IsDestroyed, true);
            _label.text = String.Empty;
            
        }
    }
}