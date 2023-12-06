using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

/*    public Task<GameObject> Instantiate(string path, Vector3 at)
    {
        throw new System.NotImplementedException();
    }

    public Task<GameObject> Instantiate(string path)
    {
        throw new System.NotImplementedException();
    }

    public Task<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class
    {
        throw new System.NotImplementedException();
    }

    public Task<T> Load<T>(string address) where T : class
    {
        throw new System.NotImplementedException();
    }*/
}
