using System;

public static class EventManager
{
    public static event Action OnEnemyDeath;
    public static event Action<int> OnBaseDamage;
    public static void CallOnEnemyDeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }

    public static void CallOnBaseDamageEvent(int damaage)
    {
        OnBaseDamage?.Invoke(damaage);
    }
}
