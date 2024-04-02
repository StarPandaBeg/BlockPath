using StarPanda.MapEditor.Runtime;
using UnityEngine;

namespace Game.Levels.Finish
{
    public abstract class FinishCondition : ScriptableObject
    {
        public virtual void InitCondition(FinishManager manager, LevelMap map) {}
        public virtual void DestroyCondition(FinishManager manager, LevelMap map) {}
        public abstract bool Check();
    }
}