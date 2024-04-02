using System;
using UnityEngine;

namespace Game.Input
{
    public class SwipeDetector
    {
        private const float DistanceMinThreshold = 0.17f;
        private const float TimeMaxThreshold = 0.5f;
    
        private static Vector2 _swipeStartPosition;
        private static Vector2 _swipeEndPosition;
        private static float _swipeStartTime;

        public static bool DetectSwipe(out Vector2Int direction) {
            direction = Vector2Int.zero;
        
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                if (!ResolveTouch(touch)) continue;
                if (CalculateSwipeDirection(out direction)) return true;
            }

            return false;
        }

        private static bool ResolveTouch(Touch touch) {
            if (touch.phase == TouchPhase.Began)
            {
                _swipeStartPosition = new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.width);
                _swipeStartTime = Time.time;
            }
        
            _swipeEndPosition = new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.width);
            return touch.phase == TouchPhase.Ended;
        }
    
        private static bool CalculateSwipeDirection(out Vector2Int direction)
        {
            direction = Vector2Int.zero;
            if (Time.time - _swipeStartTime > TimeMaxThreshold) return false;

            var swipe = _swipeEndPosition - _swipeStartPosition;
            if (swipe.magnitude < DistanceMinThreshold) return false;

            if (Math.Abs(swipe.x) > Math.Abs(swipe.y)) {
                direction.x = swipe.x > 0 ? 1 : -1;
                return true;
            }
        
            direction.y = swipe.y > 0 ? 1 : -1;
            return true;
        }
    }
}
