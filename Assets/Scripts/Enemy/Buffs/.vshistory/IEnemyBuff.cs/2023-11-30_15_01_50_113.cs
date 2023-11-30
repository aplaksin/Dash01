
public interface IEnemyBuff
{
    EnemyBuffType Type { get; set; }
    float Value { get; set; }
    void ApplyBuff(Enemy enemy);
    void RemoveBuff(Enemy enemy);
}
