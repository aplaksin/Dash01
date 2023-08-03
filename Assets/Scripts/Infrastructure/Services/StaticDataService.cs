using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StaticDataService : IStaticDataService
{
    private const string MonstersDataPath = "StaticData/Monsters";
    private const string LevelsDataPath = "StaticData/Levels";
    private const string StaticDataWindowPath = "StaticData/UI/WindowStaticData";

    private Dictionary<MonsterType, MonsterStaticData> _monsters;
    private Dictionary<string, LevelStaticData> _levelData;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;

    public void Load()
    {
        _monsters = Resources.LoadAll<MonsterStaticData>(MonstersDataPath).ToDictionary(x => x.MonsterTypeId, x => x);
        _levelData = Resources.LoadAll<LevelStaticData>(LevelsDataPath).ToDictionary(x => x.LevelKey, x => x);
        _windowConfigs = Resources.Load<WindowStaticData>(StaticDataWindowPath).Configs.ToDictionary(x => x.WindowId, x => x);
    }

    public MonsterStaticData GetMonsterDstaByType(MonsterType typeId)
    {
        return _monsters.TryGetValue(typeId, out MonsterStaticData monsterStaticData) ? monsterStaticData : null;
    }

    public LevelStaticData GetLevlStaticDataByKey(string sceneKey)
    {
        return _levelData.TryGetValue(sceneKey, out LevelStaticData levelStaticData) ? levelStaticData : null;
    }

    public WindowConfig GetWndowConfigById(WindowId windowId)
    {
        return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) ? windowConfig : null;
    }
}
