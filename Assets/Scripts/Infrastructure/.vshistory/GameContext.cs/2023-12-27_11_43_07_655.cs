using System.Collections.Generic;
using UnityEngine;
public class GameContext
{ 
    public int Score { get { return _score; } }
    public float SpawnEnemyDelay { get { return _spawnEnemyDelay; } }
    public GameStageStaticData CurrentStage { get { return _currentStage; } }
    public bool CanPlayerSwitchMoveDirection { get { return _canPlayerSwitchMoveDirection; } }


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
                Debug.Log("RemoveEnemyBuff newValue" + newValue);
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
        GameStageStaticData stage;
        //TODO score range, not constant score
         _gameStageByScore.TryGetValue(_score, out stage);

        if (stage != null)
        {
            EventManager.CallOnChangeGameStage(stage);
            _currentStage = stage;
            _spawnEnemyDelay = stage.SpawnDelay;
        }

        EventManager.CallOnScoreChanged(_score);
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
