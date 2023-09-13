using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProbability", menuName = "StaticData/Enemy/SpawnProbability")]
public class SpawnProbabilityByType : ScriptableObject
{
    public EnemyType EnemyType;
    public float Probability;

}
