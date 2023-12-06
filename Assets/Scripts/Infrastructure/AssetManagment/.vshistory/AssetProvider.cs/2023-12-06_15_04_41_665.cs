using UnityEngine;


public class AssetProvider : IAssetProvider
{


    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 spawnPoint)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    public AudioClip GetAudioClip(string path)
    {
        return Resources.Load<AudioClip>(path);
    }

    public void Cleanup()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize()
    {
        throw new System.NotImplementedException();
    }

}
