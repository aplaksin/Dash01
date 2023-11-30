
public class EnemySpeedBuff : IEnemyBuff
{

    public EnemySpeedBuff(float speedMultiplier)
    {
        _speedMultiplier = speedMultiplier;
    }

    private float _speedMultiplier = 3f;

    public void ApplyBuff(Enemy enemy)
    {
        if(enemy.SpeedMultiplier == 1)
        {
            enemy.SpeedMultiplier = _speedMultiplier;
        }
        else
        {
            enemy.SpeedMultiplier += _speedMultiplier;
        }
    }

    public void RemoveBuff(Enemy enemy)
    {
        if (enemy.SpeedMultiplier == 1)
        {
            enemy.SpeedMultiplier = 1;
        }
        else
        {
            if(enemy.SpeedMultiplier == _speedMultiplier)
            {
                enemy.SpeedMultiplier = 1;
            }
            else
            {
                enemy.SpeedMultiplier -= _speedMultiplier;
            }
        }

    }
}
