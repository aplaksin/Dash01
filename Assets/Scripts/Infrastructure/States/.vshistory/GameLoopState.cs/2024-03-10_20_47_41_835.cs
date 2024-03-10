using System.Collections;
using UnityEngine;

public class GameLoopState : IParameterizedState<LevelStaticData>//TODO del loadlevelstaticdata
{
    private IGameFactory _gameFactory;
    private EnemySpawner _enemySpawner;
    private ICoroutineRunner _coroutineRunner;
    private IWindowService _windowService;
    private IAudioService _audioService;
    //private LevelStaticData _levelStaticData;
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
        //_levelStaticData = levelStaticData;
        CreateTutorial();
        _enemySpawner = new EnemySpawner(_gameFactory);
        //_enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(Game.GameContext.SpawnEnemyDelay));//TODO check SpawnEnemyDelay
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies());//TODO check SpawnEnemyDelay

        _damageBorder = CreateDamageBorder();
        EventManager.OnDamage += _damageBorder.ShowHide;

        _audioService.PlayLevelMusic();

        
    }

    private void OnGameOver()
    {
        Debug.Log("OnGameOver");
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

    private void CreateTutorial()
    {
        _gameFactory.CreateTutorial();
    }

    //private  IEnumerator SpawnEnemies(float spawnDelay)
    private  IEnumerator SpawnEnemies()
    {

        while (true)
        {
            Enemy enemy = _enemySpawner.SpawnEnemy(Game.GameContext.CurrentStage);
            enemy.gameObject.SetActive(true);
            Game.GameContext.AddActiveEnemy(enemy);
            //Debug.Log(Game.GameContext.GetEnemyesCount());
            //Game.GameContext.ApplyEnemyBuffs();
            yield return new WaitForSeconds(Game.GameContext.SpawnEnemyDelay);
        }
    }
}
