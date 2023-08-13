public interface IStaticDataService : IService
{
    MonsterStaticData GetMonsterDstaByType(MonsterType typeId);
    void Load();
    LevelStaticData GetLevelStaticDataByKey(string sceneKey);
    WindowConfig GetWndowConfigById(WindowId shop);
}