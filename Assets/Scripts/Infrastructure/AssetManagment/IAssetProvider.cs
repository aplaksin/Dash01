using UnityEngine;

public interface IAssetProvider : IService
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 spawnPoint);

    AudioClip GetAudioClip(string path);

/*    Task<GameObject> Instantiate(string path, Vector3 at);
    Task<GameObject> Instantiate(string path);
    Task<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class;*/
    void Cleanup();
    /*Task<T> Load<T>(string address) where T : class;*/
    void Initialize();
}