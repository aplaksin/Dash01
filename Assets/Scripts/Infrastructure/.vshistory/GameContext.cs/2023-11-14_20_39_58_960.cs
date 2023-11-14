using System.Collections.Generic;
using UnityEngine;
public class GameContext
{
    private int _playerHP;
    private int _score = 0;
    private float _spawnEnemyDelay = 2f;
    private GameStageStaticData _currentStage;
    private Dictionary<int, GameStageStaticData> _gameStageByScore = new Dictionary<int, GameStageStaticData>();
    private IAudioService _audioService;
    public int Score { get { return _score; } }
    public float SpawnEnemyDelay { get { return _spawnEnemyDelay; } }
    public GameStageStaticData CurrentStage { get { return _currentStage; } }

    public GameContext(LevelStaticData levelStaticData, IAudioService audioService)
    {
        _playerHP = levelStaticData.PlayerHP;
        ConstructGameProgressionStages(levelStaticData.GameStageStaticDatas);
        GameStageStaticData stage;
        _gameStageByScore.TryGetValue(_score, out stage);
        SetActiveStage(stage);
        _spawnEnemyDelay = stage.SpawnDelay;
        _audioService = audioService;
        SubscribeOnEvents();
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
            _audioService.PlaySFX();
        }
        
    }

    public void Clear()
    {
        UnsubscribeOnEvents();
    }



}
