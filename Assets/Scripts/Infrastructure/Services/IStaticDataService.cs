public interface IStaticDataService : IService
{
    EnemyStaticData GetEnemyDataByType(EnemyType typeId);
    ProjectileStaticData GetProjectileDataByType(ProjectileType typeId);
    void Load();
    LevelStaticData GetLevelStaticDataByKey(string sceneKey);
    WindowConfig GetWndowConfigById(WindowId shop);
}