using UnityEngine;

[CreateAssetMenu(fileName = "GameStageStaticData", menuName = "StaticData/GameStage")]
public class GameStageStaticData : ScriptableObject
{
    public int ScoreStage = 0;
    public float EnemySpeedScale = 0f;
    public int AdditionalDamage = 0;
    public float SpawnDelay = 0;
}
