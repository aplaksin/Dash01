﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelStaticData", menuName ="StaticData/Level")]
public class LevelStaticData : ScriptableObject
{
    public string LevelKey;
    public Vector2 PlayerSpawnCoords;
    public GameGridStaticData GameGridData;
    public List<GameGridStaticData> GameGridStaticDataList = new List<GameGridStaticData>();
    public int PlayerHP;
    public int ScoreMultiplier = 25;
    public bool CanPlayerSwitchMoveDirection = false;
    //TODO del SpawnProbabilityByType
    //public SpawnProbabilityByType[] EnemyTypes;
    public GameStageStaticData[] GameStageStaticDatas;
    public AudioClip LevelMusic;
 }