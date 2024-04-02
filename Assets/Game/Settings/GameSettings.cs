using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private AudioMixer _mixer;

        private const float _normalVolume = -0.05f;
        private const float _mutedVolume = -80f;
    
        private bool _musicEnabled = true;
        private bool _soundEnabled = true;
        private bool _vibroEnabled = true;

        public bool MusicEnabled => _musicEnabled;
        public bool SoundEnabled => _soundEnabled;
        public bool VibroEnabled => _vibroEnabled;

        public Action<bool> MusicStateChange;
        public Action<bool> SoundStateChange;
        public Action<bool> VibroStateChange;

        public void SetMusicState(bool state) {
            SetMusicState(state, false);
        }

        public void SetMusicState(bool state, bool silent) {
            _musicEnabled = state;
            _mixer.SetFloat("MusicVolume", state ? _normalVolume : _mutedVolume);
            PlayerPrefs.SetInt("settings_music", _musicEnabled ? 1 : 0);
        
            if (!silent) MusicStateChange?.Invoke(state);
        }
        
        public void SetSoundState(bool state) {
            SetSoundState(state, false);
        }
    
        public void SetSoundState(bool state, bool silent) {
            _soundEnabled = state;
            _mixer.SetFloat("SFXVolume", state ? _normalVolume : _mutedVolume);
            PlayerPrefs.SetInt("settings_sound", _soundEnabled ? 1 : 0);
        
            if (!silent) SoundStateChange?.Invoke(state);
        }
        
        public void SetVibroState(bool state) {
            SetVibroState(state, false);
        }
    
        public void SetVibroState(bool state, bool silent) {
            _vibroEnabled = state;
            PlayerPrefs.SetInt("settings_vibro", _vibroEnabled ? 1 : 0);
        
            if (!silent) VibroStateChange?.Invoke(state);
        }
    }
}
