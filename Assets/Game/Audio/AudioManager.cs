using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private bool _persistent;
        [SerializeField] private string _persistenceId;
        [Space]
        [SerializeField] private bool _oneShot;
        [SerializeField] private Sound[] _sounds;
        
        public AudioClip CurrentClip => _source.clip;
        
        private readonly Dictionary<string, Sound> _soundDb = new();
        private AudioSource _source;
        private bool _invalid;
        
        private static readonly List<string> Registry = new();

        public void Play(string soundName) {
            Play(soundName, false);
        }

        public void Play(string soundName, bool stop) {
            if (stop) _source.Stop();
            if (_oneShot) {
                _source.PlayOneShot(_soundDb[soundName].Clip);
                return;
            }

            _source.clip = _soundDb[soundName].Clip;
            _source.loop = _soundDb[soundName].Loop;
            _source.Play();
        }

        public void PlayOrContinue(string soundName) {
            if (_source.clip == _soundDb[soundName].Clip && _source.isPlaying) return;
            Play(soundName);
        }

        public bool TryPlay(string soundName, bool stop = false) {
            if (!_soundDb.ContainsKey(soundName)) return false;
            Play(soundName, stop);
            return true;
        }
        
        private void Awake() {
            if (_persistent) {
                if (Registry.Contains(_persistenceId)) {
                    _invalid = true;
                    Destroy(this);
                    return;
                }
                
                DontDestroyOnLoad(this);
                Registry.Add(_persistenceId);
            }
            _source = GetComponent<AudioSource>();
            
            RefreshDB();
        }

        private void OnDestroy() {
            if (!_invalid) Registry.Remove(_persistenceId);
        }

        private void RefreshDB() {
            _soundDb.Clear();
            foreach (var sound in _sounds) {
                _soundDb.Add(sound.Name, sound);
            }
        }
    }
}
