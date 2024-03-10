using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySpawnProbability
{
    public EnemyType enemyType;
    public float probability;

    public string Debug()
    {
        return enemyType.ToString() + " " + probability.ToString();
    }
}
