﻿using System.Collections;
using UnityEngine;

public class GameLoopState : IParameterizedState<LevelStaticData>
{
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    private ICoroutineRunner _coroutineRunner;
    private IWindowService _windowService;
    private IAudioService _audioService;
    private LevelStaticData _levelStaticData;
    private Coroutine _enemySpawnCoroutine;
    private DamageBorder _damageBorder;

    public GameLoopState(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IWindowService windowService, IAudioService audioService)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _windowService = windowService;
        _audioService = audioService;
    }

    public void Enter(LevelStaticData levelStaticData)
    {
        _levelStaticData = levelStaticData;
        _enemySpawner = new EnemySpawner(_gameFactory);
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(Game.GameContext.SpawnEnemyDelay));
        EventManager.OnGameOver += OnGameOver;

        _damageBorder = CreateDamageBorder();
        EventManager.OnDamage += _damageBorder.ShowHide;

        _audioService.PlayLevelMusic();
    }

    private void OnGameOver()
    {
        _windowService.OpenWindowById(WindowId.GameOver);
    }

    public void Exit()
    {
        _coroutineRunner.StopCoroutine(_enemySpawnCoroutine);
        EventManager.OnGameOver -= OnGameOver;
        EventManager.OnDamage -= _damageBorder.ShowHide;
    }

    private DamageBorder CreateDamageBorder()
    {
        GameObject damageBorderObj = _gameFactory.CreateDamageBorder();
         
        return damageBorderObj.GetComponent<DamageBorder>();

    }

    private  IEnumerator SpawnEnemies(float spawnDelay)
    {

        while (true)
        {
            Enemy enemy = _enemySpawner.SpawnEnemy(Game.GameContext.CurrentStage);
            enemy.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
