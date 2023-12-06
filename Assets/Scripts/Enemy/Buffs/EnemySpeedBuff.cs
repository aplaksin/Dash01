
public class EnemySpeedBuff : IEnemyBuff
{

    public EnemySpeedBuff(float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
    }

    private float _speedMultiplier = 0.2f;

    public EnemyBuffType Type => EnemyBuffType.Speed;

    public float Value => _speedMultiplier;
}
