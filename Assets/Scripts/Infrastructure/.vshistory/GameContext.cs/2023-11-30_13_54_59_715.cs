using System.Collections.Generic;
using UnityEngine;
public class GameContext
{ 
    public int Score { get { return _score; } }
    public float SpawnEnemyDelay { get { return _spawnEnemyDelay; } }
    public GameStageStaticData CurrentStage { get { return _currentStage; } }


    private int _playerHP;
    private int _score = 0;
    private float _spawnEnemyDelay = 2f;
    private GameStageStaticData _currentStage;
    private Dictionary<int, GameStageStaticData> _gameStageByScore = new Dictionary<int, GameStageStaticData>();
    private IAudioService _audioService;
    private IAssetProvider _assetProvider;
    private Dictionary<int, Enemy> _activeEnemyes = new Dictionary<int, Enemy>();
    private List<IEnemyBuff> _enemyBuffs =  new List<IEnemyBuff>();

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
    }

    public void AddEnemyBuff(List<IEnemyBuff> buffList)
    {
        foreach(IEnemyBuff enemyBuff in buffList)
        {
            _enemyBuffs.Add(enemyBuff);
        }
        
    }

    public void RemoveEnemyBuff(List<IEnemyBuff> buffList)
    {
        foreach (IEnemyBuff enemyBuff in buffList)
        {
            _enemyBuffs.Remove(enemyBuff);
        }
    }

    public void ApplyEnemyBuffs()
    {
        foreach(Enemy enemy in _activeEnemyes.Values)
        {
            foreach(IEnemyBuff buff in _enemyBuffs)
            {
                buff.ApplyBuff(enemy);
            }
        }
    }

    public void AddActiveEnemy(Enemy enemy)
    {
        _activeEnemyes.Add(enemy.Id, enemy);
    }

    public void RemoveActiveEnemy(Enemy enemy)
    {
        _activeEnemyes.Remove(enemy.Id);
    }

    public int GetEnemyesCount()
    {
        return _activeEnemyes.Count;
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
