using UnityEngine;

namespace UI.Tutorial
{
    public class TutorialPrefab : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        public Renderer Renderer => _renderer;
    }
}