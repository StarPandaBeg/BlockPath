using System;
using UnityEngine;

namespace Game.Levels.Finish
{
    public abstract class FinishPoint : MonoBehaviour
    {
        public Action FinishEvent;
        public Action<bool> OnStateChange;
        
        public abstract bool Activated { get; }
        public abstract void Activate();
        public abstract void Deactivate();

        public void Finish() {
            FinishEvent?.Invoke();
        }
    }
}