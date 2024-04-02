using System.Collections;
using Game.Levels;

namespace Game.StateMachine
{
    public abstract class State
    {
        public virtual bool AutoTransition { get; }
        protected GameStateMachine StateMachine;

        public IEnumerator Enter(GameStateMachine machine) {
            StateMachine = machine;
            yield return OnEnter();
        }

        public IEnumerator Leave(GameStateMachine machine) {
            StateMachine = machine;
            yield return OnLeave();
        }

        public void Update(GameStateMachine machine) {
            StateMachine = machine;
            OnUpdate();
        }

        public State Signal(string signal) {
            return OnSignal(signal);
        }

        protected virtual IEnumerator OnEnter() {yield break;}
        protected virtual IEnumerator OnLeave() {yield break;}
        protected virtual void OnUpdate() {}
        protected virtual State OnSignal(string signal) {
            return null;
        }
    }
}
