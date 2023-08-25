using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector);
    GameObject CreateHud();
    void CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector, GameObject player);

    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }

}