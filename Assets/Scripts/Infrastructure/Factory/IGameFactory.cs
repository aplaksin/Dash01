using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    void Construct(Vector3 scaleVector);
    GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords, GameContext gameContext);
    Enemy CreateEnemy(Vector2 spawnPoint, EnemyType enemyType, GameStageStaticData stage, bool isTutorial = false);
    Projectile CreateProjectile(Vector2 spawnPoint);
    GameObject CreateHud();
    GameObject CreateTutorialImage();
    GameObject CreateDamageBorder();
    Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector);
    Dictionary<Vector2, Vector3> BlocksCoords { get ; }
}