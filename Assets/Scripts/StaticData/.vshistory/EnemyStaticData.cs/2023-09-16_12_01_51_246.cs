using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStaticData", menuName = "StaticData/Enemy")]
public class EnemyStaticData : ScriptableObject
{
    [Range(1f, 100f)]
    public int Hp;

    [Range(1f, 30f)]
    public int Damage;

    [Range(0.1f, 10f)]
    public float MoveSpeed;

    public EnemyType Type;

    public int Score;

}
