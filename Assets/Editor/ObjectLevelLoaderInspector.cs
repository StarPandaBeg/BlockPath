using Game.Levels.Loaders;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ObjectLevelLoader))]
    public class ObjectLevelLoaderInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var loader = (ObjectLevelLoader)target;
            if (GUILayout.Button("Reload")) {
                loader.Reload();
            }
        }
    }
}