using UnityEngine;

[CreateAssetMenu(fileName = "GameProgressionStaticData", menuName = "StaticData/GameProgression")]
public class GameStageStaticData : ScriptableObject
{
    public int ScoreStage = 0;
    public float EnemySpeedScale = 0f;
    public int AdditionalDamage = 0;
}
