using System.Collections.Generic;
using UnityEngine;
public class GameContext
{
    private int _playerHP;
    private int _score = 0;
    private GameStageStaticData _currentStage;
    private Dictionary<int, GameStageStaticData> _gameProgressionByScore = new Dictionary<int, GameStageStaticData>();

    public int Score { get { return _score; } }
    public GameStageStaticData CurrentStage { get { return _currentStage; } }

    public GameContext(LevelStaticData levelStaticData)
    {
        _playerHP = levelStaticData.PlayerHP;
        ConstructGameProgressionStages(levelStaticData.GameStageStaticDatas);
        GameStageStaticData stage;
        _gameProgressionByScore.TryGetValue(_score, out stage);
        SetActiveStage(stage);
        SubscribeOnEvents();
    }

    private void ConstructGameProgressionStages(GameStageStaticData[] gameProgressionsArr )
    {
        foreach(GameStageStaticData data in gameProgressionsArr)
        {
            bool isAdded = _gameProgressionByScore.TryAdd(data.ScoreStage, data);
            if(!isAdded)
            {
                Debug.LogError($"cant add GameProgressionStaticData, scoreStage already exist - {data.ScoreStage}");
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
         _gameProgressionByScore.TryGetValue(_score, out stage);
        if (stage != null)
        {
            EventManager.CallOnChangeGameStage(stage);
            _currentStage = stage;
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
        }
        
    }

    public void Clear()
    {
        UnsubscribeOnEvents();
    }



}
