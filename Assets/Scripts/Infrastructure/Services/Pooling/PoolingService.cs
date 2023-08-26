using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PoolingService : IPoolingService
{
    private IAssetProvider _assetProvider;
    private IStaticDataService _staticDataService;
    private Dictionary<EnemyType, Queue<GameObject>> _enemiesByType;
    private Dictionary<ProjectileType, Queue<GameObject>> _projectilesByType;
    private const int InitialCapacity = 10;
    private const int AddingCapacity = 3;

    public PoolingService(IAssetProvider assetProvider, IStaticDataService staticDataService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;
        
    }

   public void Construct()
    {
        CreateEnemiesPool();
        //CreateProjectilesPool();
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
            GameObject enemy = queue.Dequeue();
            //enemy.active = true;
            return enemy;
        }
        else
        {
            string path = AssetPath.GetProjectilePathByType(projectileType);

            AddObjectsToQueue(path, ref queue, AddingCapacity);
            return queue.Dequeue();
        }
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.transform.position = Vector3.zero;
        enemy.gameObject.SetActive(false);
        _enemiesByType[enemy.Type].Enqueue(enemy.gameObject);
    }

    public void ReturnProjectile(GameObject projectile)
    {
        throw new System.NotImplementedException();
    }

    private void CreateEnemiesPool()
    {
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
        _projectilesByType = new Dictionary<ProjectileType, Queue<GameObject>>();

        foreach (ProjectileType projectileType in Enum.GetValues(typeof(ProjectileType)))
        {
            Queue<GameObject> projectilesQueue = new Queue<GameObject>(InitialCapacity);
            string path = AssetPath.GetProjectilePathByType(projectileType);

            AddObjectsToQueue(path, ref projectilesQueue, InitialCapacity);

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

    private void AddObjectsToQueue(string path, ref Queue<GameObject> queue, int count = 1)
    {
        for(int i = 0; i < count; i++)
        {
            var obj = _assetProvider.Instantiate(path);
            obj.SetActive(false);
            queue.Enqueue(obj);
        }
    }
    




}
