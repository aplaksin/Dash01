using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelStaticData", menuName ="StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    public string LevelKey;
    public Vector2 PlayerSpawnCoords;
    public GameGridStaticData GameGridData;
    public int PlayerHP;
    public SpawnProbabilityByType[] EnemyTypes;
    public GameStageStaticData[] GameStageStaticDatas;
    public Sound[] GameSounds;
 }