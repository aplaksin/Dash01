using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnProbability
{
    public EnemySpawnProbability(EnemyType type, float prob)
    {
        enemyType = type;
        probability = prob;
    }

    public EnemyType enemyType;
    public float probability;

    public string Debug()
    {
        return enemyType + " " + probability;
    }
}
