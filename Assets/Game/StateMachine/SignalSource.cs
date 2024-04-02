using UnityEngine;

namespace Game.StateMachine
{
    public class SignalSource : MonoBehaviour
    {
        public void SendSignal(string signal) {
            GameStateMachine.Instance.Signal(signal);
        }
    }
}