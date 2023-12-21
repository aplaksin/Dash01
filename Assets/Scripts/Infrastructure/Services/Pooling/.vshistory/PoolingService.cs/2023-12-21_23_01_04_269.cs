using System;
using System.Collections.Generic;
using UnityEngine;


public class PoolingService : IPoolingService
{
    private IAssetProvider _assetProvider;
    private IStaticDataService _staticDataService;
    private IAudioService _audioService;
    private Dictionary<EnemyType, Queue<Enemy>> _enemiesByType;
    private Dictionary<ProjectileType, Queue<Projectile>> _projectilesByType;
    private const int InitialCapacity = 10;
    private const int AddingCapacity = 3;

    private const string POOL_WRAPPER_NAME = "PoolWrapper";
    private const string ENEMIES_POOL_WRAPPER_NAME = "EnemiesPool";
    private const string PROJECTILES_POOL_WRAPPER_NAME = "ProjectilesPool";

    private GameObject _poolWrapper;
    private GameObject _enemiesPoolWrapper;
    private GameObject _projectilesPoolWrapper;

    private int _totalEnemiesCount = 0;

    private List<Sprite> _enemySkins = new List<Sprite>();
    private List<Sprite> _projectilesSkins = new List<Sprite>();

    public PoolingService(IAssetProvider assetProvider, IStaticDataService staticDataService, IAudioService audioService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;
        _audioService = audioService;
        
    }

   public void Construct()
    {
        CreateWrapperObjectsForPools();
        CreateEnemiesPool();
        CreateProjectilesPool();
    }


    public Enemy GetEnemyByType(EnemyType enemyType)
    {
        Queue<Enemy> queue = _enemiesByType[enemyType];

        if(queue.Count > 0)
        {
            return queue.Dequeue();
        }
        else
        {
            string path = AssetPath.GetEnemyPathByType(enemyType);

            AddEnemiesToQueue(path, ref queue, _staticDataService.GetEnemyDataByType(enemyType), AddingCapacity);
            return queue.Dequeue();
        }
        
    }

    public Projectile GetProjectileByType(ProjectileType projectileType)
    {
        Queue<Projectile> queue = _projectilesByType[projectileType];

        if (queue.Count > 0)
        {
            Projectile projectile = queue.Dequeue();
            return projectile;
        }
        else
        {
            string path = AssetPath.GetProjectilePathByType(projectileType);

            AddProjectilesToQueue(path, ref queue, _staticDataService.GetProjectileDataByType(projectileType), AddingCapacity);
            return queue.Dequeue();
        }
    }

    public void ReturnEnemy(Enemy enemy)
    {
        Game.GameContext.RemoveActiveEnemy(enemy);
        enemy.InitBaseParams();
        enemy.gameObject.transform.position = Vector3.zero;
        enemy.gameObject.SetActive(false);
        _enemiesByType[enemy.Type].Enqueue(enemy);
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.gameObject.transform.position = Vector3.zero;
        projectile.gameObject.SetActive(false);
        _projectilesByType[projectile.Type].Enqueue(projectile);
    }

    private void CreateEnemiesPool()
    {
        _enemiesByType = new Dictionary<EnemyType, Queue<Enemy>>();

        foreach (EnemyType enemyType in Enum.GetValues(typeof(EnemyType)))
        {
            Queue<Enemy> enemiesQueue = new Queue<Enemy>();
            string path = AssetPath.GetEnemyPathByType(enemyType);
            EnemyStaticData enemyStaticData = _staticDataService.GetEnemyDataByType(enemyType);
            AddEnemiesToQueue(path, ref enemiesQueue, enemyStaticData, InitialCapacity);
            

            _enemiesByType[enemyType] = enemiesQueue;

        }
        
    }

    private void CreateWrapperObjectsForPools()
    {
        _poolWrapper = new GameObject(POOL_WRAPPER_NAME);
        _poolWrapper.transform.position = Vector3.zero;

        _enemiesPoolWrapper = new GameObject(ENEMIES_POOL_WRAPPER_NAME);
        _enemiesPoolWrapper.transform.position = Vector3.zero;
        _enemiesPoolWrapper.transform.SetParent(_poolWrapper.transform);

        _projectilesPoolWrapper = new GameObject(PROJECTILES_POOL_WRAPPER_NAME);
        _projectilesPoolWrapper.transform.position = Vector3.zero;
        _projectilesPoolWrapper.transform.SetParent(_poolWrapper.transform);

    }

    private void CreateProjectilesPool()
    {
        _projectilesByType = new Dictionary<ProjectileType, Queue<Projectile>>();

        foreach (ProjectileType projectileType in Enum.GetValues(typeof(ProjectileType)))
        {
            Queue<Projectile> projectilesQueue = new Queue<Projectile>(InitialCapacity);
            string path = AssetPath.GetProjectilePathByType(projectileType);
            ProjectileStaticData projectileStaticData = _staticDataService.GetProjectileDataByType(projectileType);
            AddProjectilesToQueue(path, ref projectilesQueue, projectileStaticData, InitialCapacity);

            _projectilesByType[projectileType] = projectilesQueue;

        }

    }

    private void AddEnemiesToQueue(string path, ref Queue<Enemy> queue, EnemyStaticData data, int count = 1)
    {

        for (int i = 0; i < count; i++)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            _totalEnemiesCount++;
            obj.SetActive(false);
            Enemy enemy = obj.GetComponent<Enemy>();
            obj.name = $"{enemy.Type}-{_totalEnemiesCount}";
            obj.transform.SetParent(_enemiesPoolWrapper.transform);
            enemy.Construct(data, this, _audioService);
            queue.Enqueue(enemy);
        }
    }

    private void AddProjectilesToQueue(string path, ref Queue<Projectile> queue, ProjectileStaticData data, int count = 1)
    {

        for (int i = 0; i < count; i++)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            obj.SetActive(false);
            Projectile projectile = obj.GetComponent<Projectile>();
            obj.transform.SetParent(_projectilesPoolWrapper.transform);
            projectile.Construct(data, this, _audioService);
            queue.Enqueue(projectile);
        }
    }

/*    private void AddObjectsToQueue(string path, ref Queue<GameObject> queue, int count = 1)
    {
        for(int i = 0; i < count; i++)
        {
            var obj = _assetProvider.Instantiate(path);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
    }*/
    
}
