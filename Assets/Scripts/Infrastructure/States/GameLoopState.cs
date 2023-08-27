using System.Collections;
using UnityEngine;

public class GameLoopState : IState
{
    private GameStateMachine _gameStateMashine;
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    private ICoroutineRunner _coroutineRunner;

    public GameLoopState(GameStateMachine gameStateMashine, IGameFactory gameFactory, ICoroutineRunner coroutineRunner)
    {
        _gameStateMashine = gameStateMashine;
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        
    }

    public void Enter()
    {
/*        _enemySpawner = new EnemySpawner(_gameFactory, Game.CurrentLevelStaticData.GameGridData.GridWidth);
        GameObject enemy = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy2 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy3 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());

        enemy.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);*/
        _enemySpawner = new EnemySpawner(_gameFactory);
        _coroutineRunner.StartCoroutine(SpawnEnemies());
    }

    public void Exit()
    {

    }

    private  IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject enemy = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
            enemy.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }
}
