public interface IStaticDataService : IService
{
    public EnemyStaticData GetEnemyDataByType(EnemyType typeId);
    void Load();
    LevelStaticData GetLevelStaticDataByKey(string sceneKey);
    WindowConfig GetWndowConfigById(WindowId shop);
}