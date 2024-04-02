using System.Collections;
using Game.Settings;
using UnityEngine;

namespace Game.StateMachine.States
{
    public class EndState : State
    {
        public override bool AutoTransition => true;
        
        protected override IEnumerator OnEnter() {
            yield return StateMachine.LoadScene("End");
            StateMachine.Music.PlayOrContinue("Menu");
        }

        protected override State OnSignal(string signal) {
            switch (signal) {
                case "button_back":
                case "event_back":
                    return new MainMenuState();
            }
            return base.OnSignal(signal);
        }
    }
}