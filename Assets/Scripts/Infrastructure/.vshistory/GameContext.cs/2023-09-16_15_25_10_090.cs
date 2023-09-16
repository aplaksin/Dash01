using System.Collections.Generic;
using UnityEngine;
public class GameContext
{
    private int _playerHP;
    private int _score = 0;
    private GameProgressionStaticData _currentStage;
    private Dictionary<int, GameProgressionStaticData> _gameProgressionByScore = new Dictionary<int, GameProgressionStaticData>();

    public int Score { get { return _score; } }

    public GameContext(LevelStaticData levelStaticData)
    {
        _playerHP = levelStaticData.PlayerHP;
        ConstructGameProgressionStages(levelStaticData.GameProgressionStaticDatas);
        SubscribeOnEvents();
    }

    private void ConstructGameProgressionStages(GameProgressionStaticData[] gameProgressionsArr )
    {
        foreach(GameProgressionStaticData data in gameProgressionsArr)
        {
            bool isAdded = _gameProgressionByScore.TryAdd(data.ScoreStage, data);
            if(!isAdded)
            {
                Debug.LogError($"cant add GameProgressionStaticData, scoreStage already exist - {data.ScoreStage}");
            }
        }
    }

    private void SetActiveStage(GameProgressionStaticData stage)
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
        GameProgressionStaticData stage;
         _gameProgressionByScore.TryGetValue(_score, out stage);
        if (stage != null)
        {
            EventManager.CallOnChangeGameStage(stage);
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
