﻿using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private List<Vector2> _spawnCoordinaresList = new List<Vector2>();
    private const int ENEMY_Y_SPAWN_POINT = 10;
    private readonly IGameFactory _gameFactory;
    private readonly EnemyType[] _enemyTypes;
    private readonly float[] _probabilities;
    public EnemySpawner(IGameFactory gameFactory, SpawnProbabilityByType[] spawnProbabilityByTypes)
    {
        _gameFactory = gameFactory;
        foreach (Vector2 key in gameFactory.BlocksCoords.Keys)
        {
            _spawnCoordinaresList.Add(new Vector2(key.x, ENEMY_Y_SPAWN_POINT));

        }


        _enemyTypes = new EnemyType[spawnProbabilityByTypes.Length];
        _probabilities = new float[spawnProbabilityByTypes.Length];

        for(int i = 0; i < spawnProbabilityByTypes.Length; i++)
        {
            _enemyTypes[i] = spawnProbabilityByTypes[i].EnemyType;
            _probabilities[i] = spawnProbabilityByTypes[i].Probability;
        }

    }

    public GameObject SpawnEnemy()
    {
        EnemyType enemyType = RandomWithRobabilitySelector.GetRandom<EnemyType>(_enemyTypes, _probabilities);
        GameObject enemy = _gameFactory.CreateEnemy(GetRandomSpawnPoint(), enemyType);
        return enemy;
    }

    //private void Get

    public Vector2 GetRandomSpawnPoint()
    {
        return _spawnCoordinaresList[Random.Range(0, _spawnCoordinaresList.Count)];
    }


}
