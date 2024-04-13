using System;
using System.Diagnostics;
using UnityEngine;
public static class EventManager
{
    public static event Action <int>OnEnemyDeath;
    public static event Action OnTutorialEnemyDeath;
    public static event Action <int>OnScoreChanged;
    public static event Action <int>OnHpChanged;
    public static event Action<int> OnDamage;
    public static event Action OnGameOver;
    public static event Action OnLevelLoaded;
    public static event Action<GameStageStaticData> OnChangeGameStage;

    public static void CallOnChangeGameStage(GameStageStaticData stage)
    {
        //UnityEngine.Debug.Log(stage);//TODO
        OnChangeGameStage?.Invoke(stage);
    }

    public static void CallOnEnemyDeathEvent(int score)
    {
        OnEnemyDeath?.Invoke(score);
    }
    public static void CallOnTutorialEnemyDeathEvent()
    {
        UnityEngine.Debug.Log("fffff");
        OnTutorialEnemyDeath?.Invoke();
    }

    public static void CallOnDamageEvent(int damaage)
    {
        OnDamage?.Invoke(damaage);
    }

    public static void CallOnScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void CallOnHpChanged(int hp)
    {
        OnHpChanged?.Invoke(hp);
    }

    public static void CallOnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void CallOnLevelLoaded()
    {
        OnLevelLoaded?.Invoke();
    }
}
