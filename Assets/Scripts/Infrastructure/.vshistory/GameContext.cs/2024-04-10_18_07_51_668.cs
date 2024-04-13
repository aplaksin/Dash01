using System.Collections.Generic;
using UnityEngine;
public class GameContext
{
    public bool IsPlayerAuthorised = false;
    public int Score { get { return _score; } }
    public float SpawnEnemyDelay { get { return _spawnEnemyDelay; } }
    public GameStageStaticData CurrentStage { get { return _currentStage; } }

    public int ScoreUIMultiplier { get { return _scoreUIMiltiplier; } }
    //TODO remove just for tests
    public bool CanPlayerSwitchMoveDirection { get { return _canPlayerSwitchMoveDirection; } set { _canPlayerSwitchMoveDirection = value; } }


    private int _playerHP;
    private int _score = 0;
    private float _spawnEnemyDelay = 2f;
    private GameStageStaticData _currentStage;
    private Dictionary<int, GameStageStaticData> _gameStageByScore = new Dictionary<int, GameStageStaticData>();
    private IAudioService _audioService;
    private IAssetProvider _assetProvider;
    private Dictionary<string, Enemy> _activeEnemies = new Dictionary<string, Enemy>();
    //private List<IEnemyBuff> _enemyBuffs =  new List<IEnemyBuff>();
    private Dictionary<EnemyBuffType, float> _enemyBuffsByType = new Dictionary<EnemyBuffType, float>();
    private bool _canPlayerSwitchMoveDirection;
    private GameStageStaticData _interpolationStage;
    private int _scoreUIMiltiplier;
    public GameContext(LevelStaticData levelStaticData, IAudioService audioService, IAssetProvider assetProvider)
    {
        _playerHP = levelStaticData.PlayerHP;
        ConstructGameProgressionStages(levelStaticData.GameStageStaticDatas);
        GameStageStaticData stage;
        _gameStageByScore.TryGetValue(_score, out stage);
        SetActiveStage(stage);
        _spawnEnemyDelay = stage.SpawnDelay;
        _audioService = audioService;
        SubscribeOnEvents();
        _assetProvider = assetProvider;
        _canPlayerSwitchMoveDirection = levelStaticData.CanPlayerSwitchMoveDirection;
        _scoreUIMiltiplier = levelStaticData.ScoreMultiplier;
    }

    public void AddEnemyBuff(List<IEnemyBuff> buffList)
    {
        foreach(IEnemyBuff enemyBuff in buffList)
        {
            //_enemyBuffs.Add(enemyBuff);
            if(_enemyBuffsByType.ContainsKey(enemyBuff.Type))
            {
                float newValue = _enemyBuffsByType[enemyBuff.Type] + enemyBuff.Value;
                _enemyBuffsByType[enemyBuff.Type] = newValue;
            }
            else
            {
                _enemyBuffsByType[enemyBuff.Type] = enemyBuff.Value;
            }

        }
        
    }

    public void RemoveEnemyBuff(List<IEnemyBuff> buffList)
    {
        foreach (IEnemyBuff enemyBuff in buffList)
        {

            if (_enemyBuffsByType.ContainsKey(enemyBuff.Type))
            {
                float newValue = _enemyBuffsByType[enemyBuff.Type] - enemyBuff.Value;
                _enemyBuffsByType[enemyBuff.Type] = newValue;
                
            }

        }
    }

    public float GetBuffValueByType(EnemyBuffType buffType)
    {
        if(_enemyBuffsByType.ContainsKey(buffType))
        {
            return _enemyBuffsByType[buffType];
        }
        else
        {
            return 0;
        }
    }


    public void AddActiveEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy.Id, enemy);
    }

    public void RemoveActiveEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy.Id);
    }

    public int GetEnemiesCount()
    {
        return _activeEnemies.Count;
    }

    public void Clear()
    {
        UnsubscribeOnEvents();
    }

    private void ConstructGameProgressionStages(GameStageStaticData[] gameStagesArr )
    {
        foreach(GameStageStaticData data in gameStagesArr)
        {
            bool isAdded = _gameStageByScore.TryAdd(data.ScoreStage, data);
            if(!isAdded)
            {
                Debug.LogError($"cant add GameStageStaticData, scoreStage already exist - {data.ScoreStage}");
            }
        }
    }

    private void SetActiveStage(GameStageStaticData stage)
    {
        _currentStage = stage;
        _interpolationStage = stage;
    }

    private void SubscribeOnEvents()
    {
        EventManager.OnEnemyDeath += OnScoreChanged;
        EventManager.OnDamage += OnHpChanged;
    } 
    private void UnsubscribeOnEvents()
    {
        EventManager.OnEnemyDeath -= OnScoreChanged;
        EventManager.OnDamage -= OnHpChanged;
    }

    private void OnScoreChanged(int score)
    {
        _score += score;

        /*        GameStageStaticData stage;
                //TODO score range, not constant score
                 _gameStageByScore.TryGetValue(_score, out stage);

                if (stage != null)
                {
                    EventManager.CallOnChangeGameStage(stage);
                    _currentStage = stage;
                    _spawnEnemyDelay = stage.SpawnDelay;
                }*/

        TestGameStageInterpolation(score);

        EventManager.CallOnScoreChanged(_score);
    }

    private void TestGameStageInterpolation(int score)
    {
        //TODO переиспользовать gameStageStaticData а не бахать все время заново
        //l;l;l;l
        //alpha = sqrt(score) / 32 # на 1000 скоре альфа будет == 1 и мы будем на максимуме сложности
        //current_parameter = (1 - alpha) * initial_parameter + alpha * end_parameter
        GameStageStaticData gameStageStaticData = ScriptableObject.CreateInstance<GameStageStaticData>();
        gameStageStaticData.enemySpawnProbabilities = new EnemySpawnProbability[_interpolationStage.enemySpawnProbabilities.Length];

        for (int i = 0; i < _interpolationStage.enemySpawnProbabilities.Length; i++)
        {
            gameStageStaticData.enemySpawnProbabilities[i] = new EnemySpawnProbability(_interpolationStage.enemySpawnProbabilities[i].enemyType, _interpolationStage.enemySpawnProbabilities[i].probability);
        }

        gameStageStaticData.EnemySpeedScale = CalcStageParamByScore(_score, _interpolationStage.EnemySpeedScale, _interpolationStage.CfEnemySpeedScale);
        gameStageStaticData.SpawnDelay = CalcStageParamByScore(_score, _interpolationStage.SpawnDelay, _interpolationStage.CfSpawnDelay);

        for(int i = 0; i < _interpolationStage.enemySpawnProbabilities.Length; i++)
        {
            gameStageStaticData.enemySpawnProbabilities[i].probability = CalcStageParamByScore(_score, _interpolationStage.enemySpawnProbabilities[i].probability, _interpolationStage.CfEnemySpawnProbabilities[i].probability);
        }

        EventManager.CallOnChangeGameStage(gameStageStaticData);
        _currentStage = gameStageStaticData;
        _spawnEnemyDelay = gameStageStaticData.SpawnDelay;

        for (int i = 0; i < _interpolationStage.enemySpawnProbabilities.Length; i++)
        {
            Debug.Log("enemySpawnProbabilities - " + gameStageStaticData.enemySpawnProbabilities[i].Debug());
        }
        

        

    }

    private float CalcStageParamByScore(int score, float initParam, float endParam)
    {
        float param = 0.0f;
        //float alpha = Mathf.Sqrt(score) / 20;

/*        float b = Mathf.Min(1, 1 / (score * 0.005f));
        float alpha = - 0.2f + Mathf.Sqrt(score) / 20 + b * 0.1f * Mathf.Sin(0.5f * score);
        alpha = Mathf.Clamp(alpha, 0, 1);

        param = (1 - alpha) * initParam + alpha * endParam;*/
        
        float b = 0.0f;

        if(score >= 15) 
        {
            b = Mathf.Min(1, 1 / (score * 0.005f));
        }

        float alpha = (5 * Mathf.Log(score) + 2 * Mathf.Sqrt(score)) * 0.0125f + b * Mathf.Max(-0.05f, 0.1f * Mathf.Sin(0.25f * score));
        param = (1 - alpha) * initParam + alpha * endParam;

        return param;
    }

    private void OnHpChanged(int hp)
    {
        _playerHP -= hp;

        EventManager.CallOnHpChanged(_playerHP);

        if(_playerHP <= 0)
        {
            
            Clear();
            EventManager.CallOnGameOver();
            _audioService.PlayGameOverMusic();
        }
        else
        {
            _audioService.PlaySFX(_assetProvider.GetAudioClip(AssetPath.PlayerDamageSFXPath));
        }
        
    }





}
