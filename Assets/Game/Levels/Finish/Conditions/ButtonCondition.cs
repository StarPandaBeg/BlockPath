using System.Collections.Generic;
using Blocks.PathPoints;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Finish.Conditions
{
    [CreateAssetMenu(menuName = "Finish Conditions/Button")]
    public class ButtonCondition : FinishCondition
    {
        private FinishManager _manager;
        private readonly List<ButtonPathPoint> _points = new();
        private int _counter;

        public override void InitCondition(FinishManager manager, LevelMap map) {
            _manager = manager;
            
            var points = map.GetComponentsInChildren<ButtonPathPoint>();
            _points.AddRange(points);
            _points.ForEach(point => point.StateChanged += OnStateChanged);
            _counter = _points.Count * 4;
        }

        public override void DestroyCondition(FinishManager manager, LevelMap map) {
            _points.ForEach(point => point.StateChanged -= OnStateChanged);
            _points.Clear();
            _counter = 0;
        }

        public override bool Check() {
            return _counter == 0;
        }

        private void OnStateChanged(bool[] state) {
            _counter--;
            if (_counter == 0) _manager.RefreshConditions();
        } 
    }
}