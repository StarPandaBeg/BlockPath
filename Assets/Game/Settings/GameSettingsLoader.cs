using System;
using UnityEngine;

namespace Game.Settings
{
    public class GameSettingsLoader : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public static GameSettingsLoader Instance { get; private set; }
        
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
            Reload();
        }

        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) return;
            Reload();
        }

        public void Reload(bool silent = false) {
            var music = PlayerPrefs.GetInt("settings_music", _gameSettings.MusicEnabled ? 1 : 0) == 1;
            var sound = PlayerPrefs.GetInt("settings_sound", _gameSettings.SoundEnabled ? 1 : 0) == 1;
            var vibro = PlayerPrefs.GetInt("settings_vibro", _gameSettings.VibroEnabled ? 1 : 0) == 1;
            
            _gameSettings.SetMusicState(music);
            _gameSettings.SetSoundState(sound);
            _gameSettings.SetVibroState(vibro);
        }
    }
}