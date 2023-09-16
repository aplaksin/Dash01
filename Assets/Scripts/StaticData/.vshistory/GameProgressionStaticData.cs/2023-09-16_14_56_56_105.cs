using UnityEngine;

[CreateAssetMenu(fileName = "GameProgressionStaticData", menuName = "StaticData/GameProgression")]
public class GameProgressionStaticData : ScriptableObject
{
    public int ScoreStage;
    public float EnemySpeedScale;
    public int AdditionalDamage;
}
