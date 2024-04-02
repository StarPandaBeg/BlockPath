using UnityEngine;

namespace Game.Util
{
    public class TargetFramerate : MonoBehaviour
    {
        [SerializeField] private int _targetFps = 30;
    
        void Start() {
            Application.targetFrameRate = _targetFps;
        }
    }
}
