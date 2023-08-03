using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    GameObject CreateHero(GameObject spawnPoint);
    GameObject CreateHud();
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
}