
public class EnemySpeedBuff : IEnemyBuff
{

    public EnemySpeedBuff(float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
    }

    private float _speedMultiplier = 0.2f;

    public void ApplyBuff(Enemy enemy)
    {

        enemy.SpeedMultiplier += _speedMultiplier;

    }

    public void RemoveBuff(Enemy enemy)
    {
        if (enemy.SpeedMultiplier == 0)
        {
            return;
        }
        else
        {

            enemy.SpeedMultiplier -= _speedMultiplier;

        }

    }
}
