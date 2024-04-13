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
    private bool _isTutorialDone = false;
    private int _tutorialEnemiesCount = 0;
    private int _maxTutorialEnemiesCount = 3;

    public GameLoopState(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, IWindowService windowService, IAudioService audioService)
    {
        _gameFactory = gameFactory;
        _coroutineRunner = coroutineRunner;
        _windowService = windowService;
        _audioService = audioService;

        if(Game.IsTutorialDone)
        {
            _isTutorialDone = true;//TODO
        }
    }

    public void Enter(LevelStaticData levelStaticData)
    {
        //_levelStaticData = levelStaticData;
        
        _enemySpawner = new EnemySpawner(_gameFactory);
        //_enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies(Game.GameContext.SpawnEnemyDelay));//TODO check SpawnEnemyDelay
        if (_isTutorialDone)
        {
            StartSpawnEnemies();
        }
        else
        {
            EventManager.OnTutorialEnemyDeath += OnTutorialEnemyDeath;
        }

        CreateTutorial();

        _damageBorder = CreateDamageBorder();
        EventManager.OnDamage += _damageBorder.ShowHide;
        EventManager.OnGameOver += OnGameOver;
        
        _audioService.PlayLevelMusic();

        
    }

    private void OnGameOver()
    {
        
        //TODO перенести остановку времени в контекст
        _windowService.OpenWindowById(WindowId.GameOver);
    }

    public void Exit()
    {
        _coroutineRunner.StopCoroutine(_enemySpawnCoroutine);
        EventManager.OnGameOver -= OnGameOver;
        EventManager.OnDamage -= _damageBorder.ShowHide;
        EventManager.OnTutorialEnemyDeath -= OnTutorialEnemyDeath;
    }

    private void StartSpawnEnemies()
    {
        _enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemies());//TODO check SpawnEnemyDelay
    }

    private void OnTutorialEnemyDeath()
    {
        
        _tutorialEnemiesCount++;
Debug.Log("_tutorialEnemiesCount " + _tutorialEnemiesCount);

        if(_maxTutorialEnemiesCount <= _tutorialEnemiesCount)
        {
            _isTutorialDone = true;
            Game.IsTutorialDone = true;
            EventManager.OnTutorialEnemyDeath -= OnTutorialEnemyDeath;
            StartSpawnEnemies();
        }
        else
        {
            SpawnTutorialEnemy();
        }
    }

    private DamageBorder CreateDamageBorder()
    {
        GameObject damageBorderObj = _gameFactory.CreateDamageBorder();
         
        return damageBorderObj.GetComponent<DamageBorder>();

    }

    private void CreateTutorial()
    {
        EventManager.OnTutorialEnemyDeath += OnTutorialEnemyDeath;
        _gameFactory.CreateTutorialImage();
        SpawnTutorialEnemy();
    }

    private void SpawnTutorialEnemy()
    {
        Enemy enemy = _enemySpawner.SpawnTutorialEnemy(Game.GameContext.CurrentStage);
        enemy.IsTutorialEnemy = true;
        enemy.gameObject.SetActive(true);
        Game.GameContext.AddActiveEnemy(enemy);
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
