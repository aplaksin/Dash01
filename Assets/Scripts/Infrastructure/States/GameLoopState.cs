using UnityEngine;

public class GameLoopState : IState
{
    private GameStateMachine _gameStateMashine;
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    
    public GameLoopState(GameStateMachine gameStateMashine, IGameFactory gameFactory)
    {
        _gameStateMashine = gameStateMashine;
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        _enemySpawner = new EnemySpawner(_gameFactory, Game.CurrentLevelStaticData.GameGridData.GridWidth);
        GameObject enemy = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy2 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());
        GameObject enemy3 = _gameFactory.CreateEnemy(_enemySpawner.GetRandomSpawnPoint());

        enemy.SetActive(true);
    }

    public void Exit()
    {
        
    }
}
