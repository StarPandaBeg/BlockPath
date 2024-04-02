using System.Collections;
using Game.Settings;
using UnityEngine;

namespace Game.StateMachine.States
{
    public class SettingsState : State
    {
        public override bool AutoTransition => true;
        
        protected override IEnumerator OnEnter() {
            yield return StateMachine.LoadScene("Settings");
            StateMachine.Music.PlayOrContinue("Menu");
        }

        protected override State OnSignal(string signal) {
            switch (signal) {
                case "button_back":
                case "event_back":
                    return new MainMenuState();
                case "button_wipe_data":
                    PlayerPrefs.DeleteAll();
                    GameSettingsLoader.Instance.Reload(true);
                    break;
            }
            return base.OnSignal(signal);
        }
    }
}