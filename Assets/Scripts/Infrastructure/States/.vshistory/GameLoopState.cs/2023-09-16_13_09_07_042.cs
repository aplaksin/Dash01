﻿using System.Collections;
using UnityEngine;

public class GameLoopState : IParameterizedState<LevelStaticData>
{
    private GameStateMachine _gameStateMashine;
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    private ICoroutineRunner _coroutineRunner;
    private IWindowService _windowService;
    private float _spawnDelay;
    private LevelStaticData _levelStaticData;
    private Coroutine _enemySpawnCoroutine;
    private GameContext _gameContext;

    public GameLoopState(GameStateMachine gameStateMashine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IWindowService windowService)
    {
        _gameStateMashine = gameStateMashine;
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _windowService = windowService;
    }

    public void Enter(LevelStaticData levelStaticData, GameContext gameContext)
    {
/*        _enemySpawner = new EnemySpawner(_gameFactory, Game.CurrentLevelStaticData.GameGridData.GridWidth);
        GameObject enemy = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy2 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy3 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());

        enemy.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);*/
        _gameContext = gameContext;
        _levelStaticData = levelStaticData;
        _enemySpawner = new EnemySpawner(_gameFactory, levelStaticData.EnemyTypes);
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(_levelStaticData.SpawnEnemyDelay, _levelStaticData.EnemyTypes));
    }

    private void OnGameOver()
    {
        
    }

    public void Exit()
    {
        _coroutineRunner.StopCoroutine(_enemySpawnCoroutine);
    }

    private  IEnumerator SpawnEnemies(float spawnDelay, SpawnProbabilityByType[] enemyTypes)
    {
        while (true)
        {

            GameObject enemy = _enemySpawner.SpawnEnemy();
            enemy.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
