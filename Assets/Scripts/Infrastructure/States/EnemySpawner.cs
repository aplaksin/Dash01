using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private List<Vector2> _spawnCoordinaresList = new List<Vector2>();
    private const int ENEMY_Y_SPAWN_POINT = 10;
    public EnemySpawner(IGameFactory gameFactory, int gridColumnsCount)
    {
        for(int i = 0; i < gridColumnsCount; i++)
        {
            _spawnCoordinaresList.Add(new Vector2(i, ENEMY_Y_SPAWN_POINT));
        }
        
    }

    public Vector2 GetRandomSpawnPoint()
    {
        return _spawnCoordinaresList[Random.Range(0, _spawnCoordinaresList.Count-1)];
    }


}