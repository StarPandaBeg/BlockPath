using System.Collections.Generic;
using Blocks.PathPoints;
using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Finish.Conditions
{
    [CreateAssetMenu(menuName = "Finish Conditions/Orb")]
    public class OrbCondition : FinishCondition
    {
        private FinishManager _manager;
        private readonly List<OrbPathPoint> _points = new();
        private int _counter;

        public override void InitCondition(FinishManager manager, LevelMap map) {
            _manager = manager;
            
            var points = map.GetComponentsInChildren<OrbPathPoint>();
            _points.AddRange(points);
            _points.ForEach(point => point.InsideEvent += OnCollected);
            _counter = _points.Count;
        }

        public override void DestroyCondition(FinishManager manager, LevelMap map) {
            _points.ForEach(point => point.InsideEvent -= OnCollected);
            _points.Clear();
            _counter = 0;
        }

        public override bool Check() {
            return _counter == 0;
        }

        private void OnCollected() {
            _counter--;
            if (_counter == 0) _manager.RefreshConditions();
        } 
    }
}