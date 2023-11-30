
public class EnemySpeedBuff : IEnemyBuff
{

    public EnemySpeedBuff(float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
    }

    private float _speedMultiplier = 0.2f;

    public void ApplyBuff(Enemy enemy)
    {

        enemy.AditionalSpeed += _speedMultiplier;

    }

    public void RemoveBuff(Enemy enemy)
    {
        if (enemy.AditionalSpeed == 0)
        {
            return;
        }
        else
        {
            enemy.AditionalSpeed -= _speedMultiplier;
        }

    }
}
