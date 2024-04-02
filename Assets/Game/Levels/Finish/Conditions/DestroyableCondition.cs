using System.Collections.Generic;
using Blocks.PathPoints;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Finish.Conditions
{
    [CreateAssetMenu(menuName = "Finish Conditions/Destroyable")]
    public class DestroyableCondition : FinishCondition
    {
        private FinishManager _manager;
        private readonly List<DestroyablePathPoint> _points = new();
        private int _counter;

        public override void InitCondition(FinishManager manager, LevelMap map) {
            _manager = manager;
            
            var points = map.GetComponentsInChildren<DestroyablePathPoint>();
            _points.AddRange(points);
            _points.ForEach(point => point.TimerEnded += OnTimerEnded);
            _counter = _points.Count;
        }

        public override void DestroyCondition(FinishManager manager, LevelMap map) {
            _points.ForEach(point => point.TimerEnded -= OnTimerEnded);
            _points.Clear();
            _counter = 0;
        }

        public override bool Check() {
            return _counter == 0;
        }

        private void OnTimerEnded() {
            _counter--;
            if (_counter == 0) _manager.RefreshConditions();
        } 
    }
}