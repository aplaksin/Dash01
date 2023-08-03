using UnityEngine;

[CreateAssetMenu(fileName ="MonsterData", menuName ="StaticData/Monster")]
public class MonsterStaticData : ScriptableObject
{
    public MonsterType MonsterTypeId;

    [Range(0f, 100f)]
    public float Hp;

    [Range(1f, 30f)]
    public float Damage;
}
