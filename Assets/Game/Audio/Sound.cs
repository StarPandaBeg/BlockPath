using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Audio
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _random;
        [SerializeField] private bool _loop;
        [SerializeField] private AudioClip[] _clips;

        public string Name => _name;
        public bool Loop => _loop;
        public AudioClip Clip => _random ? _clips[Random.Range(0, _clips.Length)] : _clips[0];
    }
}