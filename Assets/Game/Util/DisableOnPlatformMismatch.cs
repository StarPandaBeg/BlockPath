using UnityEngine;

namespace Game.Util
{
    public class DisableOnPlatformMismatch : MonoBehaviour
    {
        [SerializeField] private RuntimePlatform _runtimePlatform;
        [SerializeField] private bool _self;
        [SerializeField] private GameObject[] _objects;
        [SerializeField] private Behaviour[] _components;
        
        void OnEnable()
        {
            if (Application.platform != _runtimePlatform) {
                if (_self) {
                    gameObject.SetActive(false);
                }

                foreach (var o in _objects) {
                    o.SetActive(false);
                }
                foreach (var c in _components) {
                    c.enabled = false;
                }
            }
        }
    }
}