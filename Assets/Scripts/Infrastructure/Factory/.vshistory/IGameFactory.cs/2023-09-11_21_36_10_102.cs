using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    void Construct(Vector3 scaleVector);
    GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords);
    GameObject CreateEnemy(Vector2 spawnPoint);
    GameObject CreateProjectile(Vector2 spawnPoint);
    GameObject CreateHud();

    //GameObject CreatePauseMenu();
    Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector);

    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    //Dictionary<Vector2, Vector3> CellPositionByCoords { get ; }
    Dictionary<Vector2, Vector3> BlocksCoords { get ; }
}