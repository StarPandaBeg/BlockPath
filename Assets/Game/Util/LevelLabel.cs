
using Game.Levels;
using StarPanda.MapEditor.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class LevelLabel : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent _localizeStringEvent;
    [SerializeField] private LevelManager _levelManager;

    private LocalizedString LocalizedString => _localizeStringEvent.StringReference;
    private IntVariable IndexVariable {
        get
        {
            
            if (!LocalizedString.TryGetValue("index", out var variable)) {
                var indexVariable = new IntVariable();
                LocalizedString.Add("index", indexVariable);
                return indexVariable;
            }
            return variable as IntVariable;
        }
    }

    private void OnEnable() {
        _levelManager.LevelLoaded += OnLevelLoaded;
    }
    
    private void OnDisable() {
        _levelManager.LevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Level level, string levelName) {
        var gameLevel = (GameLevel)level;
        IndexVariable.Value = gameLevel.Index + 1;
        _localizeStringEvent.RefreshString();
    }
}
