using System;

namespace Blocks.PathPoints.Util
{
    public class FinishPoint : Game.Levels.Finish.FinishPoint
    {
        public override bool Activated => _activated;
        private bool _activated;
        
        public override void Activate() {
            _activated = true;
            OnStateChange?.Invoke(true);
        }

        public override void Deactivate() {
            _activated = false;
            OnStateChange?.Invoke(false);
        }
    }
}