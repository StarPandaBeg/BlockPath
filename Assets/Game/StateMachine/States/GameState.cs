using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Levels;
using Game.Levels.Finish;
using StarPanda.MapEditor.Runtime;
using UI.Tutorial;
using UnityEngine;

namespace Game.StateMachine.States
{
    public class GameState : State
    {
        public override bool AutoTransition => true;

        private IReadOnlyList<GameLevelStorage.LevelData> Levels => StateMachine.LevelStorage.Levels;
        private LevelMap _map;
        private FinishManager _finishManager;
        private Player.Player _player;
        private string _currentLevel;

        protected override IEnumerator OnEnter() {
            yield return StateMachine.LoadScene("Game");
            StateMachine.Music.PlayOrContinue("Game");

            _currentLevel = PlayerPrefs.GetString("last_level", StateMachine.LevelStorage.First.Name);
            if (!StateMachine.LevelStorage.IsExists(_currentLevel)) {
                _currentLevel = StateMachine.LevelStorage.First.Name;
            }
            
            _map = FindGameMap();
            _finishManager = FindFinishManager();
            
            yield return LevelManager.Instance.Load(_map, _currentLevel);
            RegisterPlayer();

            _finishManager.Finish += OnFinish;
        }

        protected override IEnumerator OnLeave() {
            _finishManager.Finish -= OnFinish;
            _player.Physics.OnBorderTouch -= OnGameOver;
            _map.Clear();
            
            PlayerPrefs.SetString("last_level", _currentLevel);
            yield break;
        }

        protected override State OnSignal(string signal) {
            switch (signal) {
                case "button_back":
                case "event_back":
                    StateMachine.ChangeState(new MainMenuState());
                    return null;
                
                case "button_restart":
                case "event_restart":
                    StateMachine.ChangeState(this);
                    return null;
            }
            return base.OnSignal(signal);
        }

        private LevelMap FindGameMap() {
            var obj = GameObject.FindObjectOfType<LevelMap>();
            if (obj == null) {
                throw new ApplicationException(
                    "The scene doesn't have a LevelMap component. Ensure that you created it properly.");
            }

            return obj;
        }

        private FinishManager FindFinishManager() {
            var obj = GameObject.FindObjectOfType<FinishManager>();
            if (obj == null) {
                throw new ApplicationException(
                    "The scene doesn't have a FinishManager component. Ensure that you created it properly.");
            }

            return obj;
        }
    
        private void RegisterPlayer() {
            _player = _map.GetComponentInChildren<Player.Player>();
            _player.Physics.OnBorderTouch += OnGameOver;
        }

        private void OnFinish() {
            _player.Finish();

            if (_currentLevel == StateMachine.LevelStorage.Last.Name) {
                StateMachine.ChangeState(new EndState());
                return;
            }
            
            var data = Levels.SkipWhile(level => level.Name != _currentLevel).Skip(1).FirstOrDefault();
            if (data.Data != null) {
                _currentLevel = data.Name;
            }
            StateMachine.ChangeState(this);
        }

        private void OnGameOver() {
            _player.Die();
            StateMachine.ChangeState(this);
        }

        protected override void OnUpdate() {
            if (UnityEngine.Input.GetKey("r")) {
                StateMachine.Signal("event_restart");
            }
        }
    }
}