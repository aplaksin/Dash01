using System;
using System.Collections.Generic;
using UnityEngine;


public class PoolingService : IPoolingService
{
    private IAssetProvider _assetProvider;
    private IStaticDataService _staticDataService;
    private Dictionary<EnemyType, Queue<GameObject>> _enemiesByType;
    private Dictionary<ProjectileType, Queue<GameObject>> _projectilesByType;
    private const int InitialCapacity = 10;
    private const int AddingCapacity = 3;
    private GameObject _poolWrapper;

    public PoolingService(IAssetProvider assetProvider, IStaticDataService staticDataService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;
        
    }

   public void Construct()
    {
        _poolWrapper = new GameObject("PoolWrapper");
        CreateEnemiesPool();
        CreateProjectilesPool();
    }


    public GameObject GetEnemyByType(EnemyType enemyType)
    {
        Queue<GameObject> queue = _enemiesByType[enemyType];

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

    public GameObject GetProjectileByType(ProjectileType projectileType)
    {
        Queue<GameObject> queue = _projectilesByType[projectileType];

        if (queue.Count > 0)
        {
            GameObject projectile = queue.Dequeue();
            //enemy.active = true;
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
        enemy.gameObject.transform.position = Vector3.zero;
        enemy.gameObject.SetActive(false);
        _enemiesByType[enemy.Type].Enqueue(enemy.gameObject);
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.gameObject.transform.position = Vector3.zero;
        projectile.gameObject.SetActive(false);
        _projectilesByType[projectile.Type].Enqueue(projectile.gameObject);
    }

    private void CreateEnemiesPool()
    {
        GameObject enemiesPoolWrapper = new GameObject("EnemiesPool");
        enemiesPoolWrapper.transform.SetParent(_poolWrapper.transform);

        _enemiesByType = new Dictionary<EnemyType, Queue<GameObject>>();

        foreach (EnemyType enemyType in Enum.GetValues(typeof(EnemyType)))
        {
            Queue<GameObject> enemiesQueue = new Queue<GameObject>();
            string path = AssetPath.GetEnemyPathByType(enemyType);
            EnemyStaticData enemyStaticData = _staticDataService.GetEnemyDataByType(enemyType);
            AddEnemiesToQueue(path, ref enemiesQueue, enemyStaticData, InitialCapacity);
            

            _enemiesByType[enemyType] = enemiesQueue;

        }
        
    }
    private void CreateProjectilesPool()
    {
        GameObject projectilesPoolWrapper = new GameObject("ProjectilesPool");
        projectilesPoolWrapper.transform.SetParent(_poolWrapper.transform);

        _projectilesByType = new Dictionary<ProjectileType, Queue<GameObject>>();

        foreach (ProjectileType projectileType in Enum.GetValues(typeof(ProjectileType)))
        {
            Queue<GameObject> projectilesQueue = new Queue<GameObject>(InitialCapacity);
            string path = AssetPath.GetProjectilePathByType(projectileType);
            ProjectileStaticData projectileStaticData = _staticDataService.GetProjectileDataByType(projectileType);
            AddProjectilesToQueue(path, ref projectilesQueue, projectileStaticData, InitialCapacity);

            _projectilesByType[projectileType] = projectilesQueue;

        }

    }

    private void AddEnemiesToQueue(string path, ref Queue<GameObject> queue, EnemyStaticData data, int count = 1)
    {

        for (int i = 0; i < count; i++)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            obj.SetActive(false);
            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.Construct(data, this);
            queue.Enqueue(obj);
        }
    }

    private void AddProjectilesToQueue(string path, ref Queue<GameObject> queue, ProjectileStaticData data, int count = 1)
    {

        for (int i = 0; i < count; i++)
        {
            GameObject obj = _assetProvider.Instantiate(path);
            obj.SetActive(false);
            Projectile projectile = obj.GetComponent<Projectile>();
            projectile.Construct(data, this);
            queue.Enqueue(obj);
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
