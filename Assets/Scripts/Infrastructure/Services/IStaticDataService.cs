public interface IStaticDataService : IService
{
    MonsterStaticData GetMonsterDstaByType(MonsterType typeId);
    void Load();
    LevelStaticData GetLevlStaticDataByKey(string sceneKey);
    WindowConfig GetWndowConfigById(WindowId shop);
}