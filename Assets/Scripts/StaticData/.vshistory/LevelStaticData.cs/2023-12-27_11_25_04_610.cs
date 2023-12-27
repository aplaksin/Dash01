﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelStaticData", menuName ="StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    public string LevelKey;
    public Vector2 PlayerSpawnCoords;
    public GameGridStaticData GameGridData;
    public List<GameGridStaticData> gameGridStaticDataList;
    public int PlayerHP;

    //TODO del SpawnProbabilityByType
    //public SpawnProbabilityByType[] EnemyTypes;
    public GameStageStaticData[] GameStageStaticDatas;
    public AudioClip LevelMusic;
 }