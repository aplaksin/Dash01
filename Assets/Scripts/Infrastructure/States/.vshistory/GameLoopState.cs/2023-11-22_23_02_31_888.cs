using System.Collections;
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
    private int eCount = 0;
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
        _enemySpawner = new EnemySpawner(_gameFactory/*, levelStaticData.EnemyTypes*/);
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(Game.GameContext.SpawnEnemyDelay));
        EventManager.OnGameOver += OnGameOver;
        
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
    }

    private  IEnumerator SpawnEnemies(float spawnDelay)
    {


        while (eCount < 4)
        {
            eCount++;
            Enemy enemy = _enemySpawner.SpawnEnemy(Game.GameContext.CurrentStage);
            enemy.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
