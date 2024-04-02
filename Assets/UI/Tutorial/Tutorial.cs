using System;
using UnityEngine;
using UnityEngine.Localization;

namespace UI.Tutorial
{
    [Serializable]
    public class Tutorial
    {
        public string Key;
        public LocalizedString Title;
        public LocalizedString Subtitle;
        public LocalizedString Text;
        public TutorialPrefab Prefab;
    }
}