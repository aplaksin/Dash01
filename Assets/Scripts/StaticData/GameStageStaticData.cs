using UnityEngine;

[CreateAssetMenu(fileName = "GameStageStaticData", menuName = "StaticData/GameStage")]
public class GameStageStaticData : ScriptableObject
{
    public int ScoreStage = 0;
    public float EnemySpeedScale = 0f;
    public int AdditionalDamage = 0;
    public float SpawnDelay = 0;
    public EnemySpawnProbability[] enemySpawnProbabilities;

    public float CfEnemySpeedScale = 0f;
    public float CfSpawnDelay = 0;
    public EnemySpawnProbability[] CfEnemySpawnProbabilities;
    public EnemyType[] GetEnemyTypes()
    {
        EnemyType[] enemyTypes = new EnemyType[enemySpawnProbabilities.Length];

        for(int i = 0; i < enemySpawnProbabilities.Length; i++)
        {
            enemyTypes[i] = enemySpawnProbabilities[i].enemyType;
        }

        return enemyTypes;
    }

    public float[] GetEnemySpawnProbabilities()
    {
        float[] probabilities = new float[enemySpawnProbabilities.Length];

        for (int i = 0; i < enemySpawnProbabilities.Length; i++)
        {
            probabilities[i] = enemySpawnProbabilities[i].probability;
        }

        return probabilities;
    }

}
