
using System.Collections;
using UnityEngine;

namespace Game.StateMachine.States
{
    public class MainMenuState : State
    {
        public override bool AutoTransition => true;
        
        protected override IEnumerator OnEnter() {
            yield return StateMachine.LoadScene("Main Menu");
            StateMachine.Music.PlayOrContinue("Menu");
        }

        protected override State OnSignal(string signal) {
            switch (signal) {
                case "button_play":
                    return new GameState();
                case "button_settings":
                    return new SettingsState();
                
                case "button_social_telegram":
                    Application.OpenURL("https://x_starpanda_x.t.me/");
                    return null;
                
                case "event_back":
                case "button_exit":
                    Application.Quit();
                    return null;
            }

            return base.OnSignal(signal);
        }
    }
}
