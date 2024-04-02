using System;
using Game.Levels;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace UI.Tutorial
{
    [RequireComponent(typeof(TutorialLoader))]
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        
        private TutorialLoader _loader;

        private void Awake() {
            _loader = GetComponent<TutorialLoader>();
        }

        private void OnEnable() {
            _levelManager.LevelLoaded += OnLevelLoaded;
        }
        
        private void OnDisable() {
            _levelManager.LevelLoaded -= OnLevelLoaded;
        }

        private void OnLevelLoaded(Level level, string name) {
            var gameLevel = (GameLevel)level;
            if (!gameLevel.HasTutorial) return;
            if (WasShownBefore(gameLevel.Tutorial.Key)) return;
            
            _loader.Show(gameLevel.Tutorial);
            MarkAsShown(gameLevel.Tutorial.Key);
        }

        private static bool WasShownBefore(string key) {
            return PlayerPrefs.HasKey($"tut_{key}");
        }
        
        private static void MarkAsShown(string key) {
            PlayerPrefs.SetInt($"tut_{key}", 1);
        }
    }
}