
public class EnemySpeedBuff : IEnemyBuff
{

    public EnemySpeedBuff(float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
    }

    private float _speedMultiplier = 0.2f;

    public void ApplyBuff(Enemy enemy)
    {

        enemy.AdditionalSpeed += _speedMultiplier;

    }

    public void RemoveBuff(Enemy enemy)
    {
        if (enemy.AdditionalSpeed == 0)
        {
            return;
        }
        else
        {
            enemy.AdditionalSpeed -= _speedMultiplier;
        }

    }
}
