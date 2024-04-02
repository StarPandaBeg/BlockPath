using System;
using System.Collections;
using UnityEngine;

namespace Blocks.PathPoints
{
    public class DestroyablePathPoint : PathPoint
    {
        [SerializeField] private int _cooldown = 10;
        [SerializeField] private float _cooldownTime = 10;
        
        public override PointType Type => _type;

        public Action<int> TimerStarted;
        public Action<int> TimerStep;
        public Action TimerEnded;

        private bool _isTimerStarted;
        private PointType _type;

        private void OnEnable() {
            _type = PointType.Solid;
        }

        private void OnDisable() {
            _type = PointType.Transparent;
        }

        protected override void OnPlayerTouch(Vector2Int side) {
            StartCoroutine(Timer());
        }

        private IEnumerator Timer() {
            if (_isTimerStarted) yield break;
            _isTimerStarted = true;

            var step = _cooldownTime / _cooldown;
            var cd = _cooldownTime;
            var currentStep = _cooldown;
            
            TimerStarted?.Invoke(currentStep);
            while (cd > 0) {
                yield return new WaitForSeconds(step);
                cd -= step;
                
                TimerStep?.Invoke(currentStep--);
            }
            TimerEnded?.Invoke();

            enabled = false;
            _isTimerStarted = false;
        }
    }
}
