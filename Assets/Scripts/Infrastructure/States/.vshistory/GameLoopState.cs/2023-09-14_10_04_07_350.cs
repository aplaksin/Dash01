using System.Collections;
using UnityEngine;

public class GameLoopState : IParameterizedState<LevelStaticData>
{
    private GameStateMachine _gameStateMashine;
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    private ICoroutineRunner _coroutineRunner;
    private float _spawnDelay;
    private LevelStaticData _levelStaticData;
    private Coroutine _enemySpawnCoroutine;
    public GameLoopState(GameStateMachine gameStateMashine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner)
    {
        _gameStateMashine = gameStateMashine;
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        
    }

    public void Enter(LevelStaticData levelStaticData)
    {
/*        _enemySpawner = new EnemySpawner(_gameFactory, Game.CurrentLevelStaticData.GameGridData.GridWidth);
        GameObject enemy = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy2 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy3 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());

        enemy.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);*/
        _levelStaticData = levelStaticData;
        _enemySpawner = new EnemySpawner(_gameFactory, levelStaticData.EnemyTypes);
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(_levelStaticData.SpawnEnemyDelay, _levelStaticData.EnemyTypes));
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
