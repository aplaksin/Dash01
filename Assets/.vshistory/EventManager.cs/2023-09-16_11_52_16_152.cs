using System;

public static class EventManager
{
    public static event Action OnEnemyDeath;
    public static event Action <int>OnScoreChanged;
    public static event Action <int>OnHpChangedChanged;
    public static event Action<int> OnBaseDamage;
    public static void CallOnEnemyDeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }

    public static void CallOnBaseDamageEvent(int damaage)
    {
        OnBaseDamage?.Invoke(damaage);
    }

    public static void CallOnScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void CallOnHpChanged(int hp)
    {
        OnHpChangedChanged?.Invoke(hp);
    }
}
