using System;

public static class EventManager
{
    public static event Action <int>OnEnemyDeath;
    public static event Action <int>OnScoreChanged;
    public static event Action <int>OnHpChanged;
    public static event Action<int> OnDamage;
    public static event Action OnGameOver;

    public static void CallOnEnemyDeathEvent(int score)
    {
        OnEnemyDeath?.Invoke(score);
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
}
