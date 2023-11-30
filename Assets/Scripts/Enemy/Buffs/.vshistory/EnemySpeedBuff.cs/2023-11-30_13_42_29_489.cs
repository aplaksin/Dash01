using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedBuff : IEnemyBuff
{
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
            enemy.SpeedMultiplier -= _speedMultiplier;
        }

    }
}
