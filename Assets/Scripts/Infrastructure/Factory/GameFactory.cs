using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.VersionControl;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GameFactory : IGameFactory
{
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    //private List<Vector2> _blocks = new List<Vector2>();
    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataService _staticDataService;
    private LevelStaticData levelStaticData;

    private Dictionary<Vector2, Vector3> _cellPositionByCoords = new Dictionary<Vector2, Vector3>();
    private Dictionary<Vector2, GameObject> _blocksByCoords = new Dictionary<Vector2, GameObject>();


    public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;

    }
    
    public void CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector, GameObject player)
    {
        
        BuildGrid(scaleVector, levelStaticData.GameGridData.GridHeight, levelStaticData.GameGridData.GridWidth, new List<Vector2>(levelStaticData.GameGridData.BlocksCoords), player, levelStaticData.GameGridData.CellSpace);
    }
    
    public GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector)
    {
        GameObject player = _assetProvider.Instantiate(AssetPath.PlayerPath);
        player.transform.localScale = scaleVector;
        //player.transform.position = new Vector3(spawnPoint.x + player.transform.localScale.x / 2, spawnPoint.y + player.transform.localScale.y / 2, 0);
        
        PlayerMove playerMove = player.transform.GetComponent<PlayerMove>();
        playerMove.Construct(_cellPositionByCoords, _blocksByCoords, spawnPoint);
        player.transform.position = spawnPoint;
        return player;
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

    private void BuildGrid(Vector3 scaleVector, int gridHeight, int gridWidth, List<Vector2> blocksList, GameObject player, float cellSpace = 0.0f)
    {
        
        GameObject grid = _assetProvider.Instantiate(AssetPath.GridPath);
        float positionByScalePointerVertical = 0.0f;
        for (int i = 0; i < gridHeight; i++)
        {
            float positionByScalePointerHorizontal = 0.0f;

            for (int j = 0; j < gridWidth; j++)
            {
                Vector2 currentCoords = new Vector2(j, i);

                GameObject prefab;

                if (blocksList.Contains(currentCoords))
                {
                    //prefab = _blockPrefab;
                    prefab = _assetProvider.Instantiate(AssetPath.BlockPath);
                }
                else
                {
                    prefab = _assetProvider.Instantiate(AssetPath.CellPath);
                }

                //var cell = Object.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                var cell = prefab;
                cell.name = $"{cell.name}-{j}-{i}";
                //cell.transform.localScale = new Vector3(cell.transform.localScale.x * scaleVector.x, cell.transform.localScale.y * scaleVector.y, cell.transform.localScale.z * scaleVector.z);
                cell.transform.localScale = scaleVector;
                cell.transform.position = new Vector3(positionByScalePointerHorizontal + cell.transform.localScale.x / 2, positionByScalePointerVertical + cell.transform.localScale.y / 2, 0);
                _cellPositionByCoords.Add(currentCoords, cell.transform.position);

                if (blocksList.Contains(currentCoords))
                {
                    _blocksByCoords.Add(currentCoords, cell);
                }

                cell.transform.SetParent(grid.transform);

                positionByScalePointerHorizontal += scaleVector.x + cellSpace;
            }

            positionByScalePointerVertical += scaleVector.y + cellSpace;
        }
        player.transform.position = _cellPositionByCoords[player.transform.position];
    }

}
