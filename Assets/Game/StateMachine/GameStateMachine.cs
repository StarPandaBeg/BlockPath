using System;
using System.Collections;
using System.Collections.Generic;
using Game.Audio;
using Game.Levels;
using Game.Settings;
using Game.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.StateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private GameLevelStorage _levelStorage;
        [SerializeField] private GameSettings _gameSettings;
        
        public static GameStateMachine Instance { get; private set; }

        public State State => _state;
        public GameLevelStorage LevelStorage => _levelStorage;
        public AudioManager Music => _musicManager;
        
        private State _state;
        private readonly Queue<State> _transitions = new();
        private bool _isTransitionInProgress;
        private AudioManager _musicManager;
    
        public void ChangeState(State state) {
            _transitions.Enqueue(state);
        }
        
        public void Signal(string signal) {
            var targetState = State?.Signal(signal);
            if (targetState != null) ChangeState(targetState);
        }

        public IEnumerator LoadScene(string scene, bool force = false) {
            var currentSceneName = SceneManager.GetActiveScene().name;
            if (scene == currentSceneName && !force) {
                _musicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
                yield break;
            }

            yield return SceneManager.LoadSceneAsync(scene);
            _musicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();
        }
        
        public IEnumerator StartLoading() {
            yield return _loadingScreen.StartLoading();
        }

        public IEnumerator EndLoading() {
            yield return _loadingScreen.EndLoading();
        }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(_loadingScreen.gameObject);
                Destroy(this);
                return;
            }
        
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start() {
            ChangeState(new MainMenuState());
        }

        private void Update() {
            if (!_isTransitionInProgress && _transitions.Count > 0) {
                StartCoroutine(TransitionTo(_transitions.Dequeue()));
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape)) { 
                Signal("event_back");
            }
            
            if (_isTransitionInProgress) return;
            _state?.Update(this);
        }

        private IEnumerator TransitionTo(State state) {
            var hasTransition = _state?.AutoTransition == true;
            
            _isTransitionInProgress = true;
            if (hasTransition) yield return StartLoading();
            yield return _state?.Leave(this);
        
            _state = state;

            yield return _state.Enter(this);
            if (hasTransition) yield return EndLoading();
            _isTransitionInProgress = false;
        }
    }
}
