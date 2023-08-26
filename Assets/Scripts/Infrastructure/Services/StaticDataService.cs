using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string EnemyDataPath = "StaticData/Enemy";
    private const string LevelsDataPath = "StaticData/Levels";
    private const string StaticDataWindowPath = "StaticData/UI/WindowStaticData";

    private Dictionary<EnemyType, EnemyStaticData> _enemies;
    private Dictionary<string, LevelStaticData> _levelData;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;

    public void Load()
    {
        _enemies = Resources.LoadAll<EnemyStaticData>(EnemyDataPath).ToDictionary(x => x.Type, x => x);
        _levelData = Resources.LoadAll<LevelStaticData>(LevelsDataPath).ToDictionary(x => x.LevelKey, x => x);
        //_windowConfigs = Resources.Load<WindowStaticData>(StaticDataWindowPath).Configs.ToDictionary(x => x.WindowId, x => x);
    }

    public EnemyStaticData GetEnemyDataByType(EnemyType typeId)
    {
        return _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData) ? enemyStaticData : null;
    }

    public LevelStaticData GetLevelStaticDataByKey(string sceneKey)
    {
        return _levelData.TryGetValue(sceneKey, out LevelStaticData levelStaticData) ? levelStaticData : null;
    }

    public WindowConfig GetWndowConfigById(WindowId windowId)
    {
        return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) ? windowConfig : null;
    }
}
