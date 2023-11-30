
public interface IEnemyBuff
{
    EnemyBuffType Type { get; }
    float Value { get; }
    void ApplyBuff(Enemy enemy);
    void RemoveBuff(Enemy enemy);
}
