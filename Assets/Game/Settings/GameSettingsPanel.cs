using Game.Audio;
using Game.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Settings
{
    public class GameSettingsPanel : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameSettings _settings;
        [Space]
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Toggle _vibroToggle;
        [Space]
        [SerializeField] private GameObject _wipeButton;

        private void Awake() {
            _musicToggle.SetIsOnWithoutNotify(_settings.MusicEnabled);
            _soundToggle.SetIsOnWithoutNotify(_settings.SoundEnabled);
            _vibroToggle.SetIsOnWithoutNotify(Vibration.HasVibrator() && _settings.VibroEnabled);
            _vibroToggle.gameObject.SetActive(Vibration.HasVibrator());
            
            _wipeButton.SetActive(PlayerPrefs.HasKey("last_level"));
        }

        private void OnEnable() {
            _settings.MusicStateChange += OnMusicStateChange;
            _settings.SoundStateChange += OnSoundStateChange;
            _settings.VibroStateChange += OnVibroStateChange;
        }
        
        private void OnDisable() {
            _settings.MusicStateChange -= OnMusicStateChange;
            _settings.SoundStateChange -= OnSoundStateChange;
            _settings.VibroStateChange -= OnVibroStateChange;
        }

        private void OnMusicStateChange(bool state) {
            _audioManager.TryPlay(state ? "UIToggleOn" : "UIToggleOff");
        }

        private void OnSoundStateChange(bool state) {
            _audioManager.TryPlay(state ? "UIToggleOn" : "UIToggleOff");
        }

        private void OnVibroStateChange(bool state) {
            _audioManager.TryPlay(state ? "UIToggleOn" : "UIToggleOff");
            if (state) Vibration.Vibrate(20);
        }
    }
}