using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameFactory : IGameFactory
{
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }


    public GameObject CreateHero(GameObject spawnPoint)
    {
        return _assetProvider.Instantiate(AssetPath.HeroPath, spawnPoint.transform.position);
    }

    public GameObject CreateHud()
    {
        return _assetProvider.Instantiate(AssetPath.HudPath);
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);

        ProgressReaders.Add(progressReader);
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
    }

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();

       
    }

}
