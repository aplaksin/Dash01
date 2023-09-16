using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    void Construct(Vector3 scaleVector);
    GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords);
    GameObject CreateEnemy(Vector2 spawnPoint, EnemyType enemyType);
    GameObject CreateProjectile(Vector2 spawnPoint);
    GameObject CreateHud();
    Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector);
    Dictionary<Vector2, Vector3> BlocksCoords { get ; }
}