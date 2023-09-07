using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public Dictionary<Vector2, Vector3> BlocksCoords { get { return _blocksCoords; } }

    //private List<Vector2> _blocks = new List<Vector2>();
    private readonly IAssetProvider _assetProvider;
    private readonly IPoolingService _poolingService;
    private readonly IStaticDataService _staticDataService;
    private LevelStaticData levelStaticData;

    private Vector3 _scaleVector;
    private Dictionary<Vector2, Vector3> _cellPositionByCoords = new Dictionary<Vector2, Vector3>();
    private Dictionary<Vector2, GameObject> _blocksByCoords = new Dictionary<Vector2, GameObject>();
    private Dictionary<Vector2,Vector3> _blocksCoords = new Dictionary<Vector2, Vector3>();

    public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPoolingService poolingService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;
        _poolingService = poolingService;
    }



    public void Construct(Vector3 scaleVector)
    {
        AddScaleVector(scaleVector);
        //TODO переделать прокидывание scaleVector мб перенести CalcScaleVector() из лоад левел сюда
        // создать метод что-то вроде CreateBaseGameObjects и там создавать грид и плеера
        // возможно стоит выделить отдельно энеми мувер

    }

    public GameObject CreateProjectile(Vector2 spawnPoint)
    {
        GameObject projectile = _poolingService.GetProjectileByType(ProjectileType.Base);
        projectile.transform.position = spawnPoint;
        return projectile;
    }

    public GameObject CreateEnemy(Vector2 spawnPoint)
    {
        GameObject enemy = _poolingService.GetEnemyByType(EnemyType.Base);
        enemy.transform.localScale = _scaleVector;
        enemy.transform.position = new Vector3(_cellPositionByCoords[new Vector2(spawnPoint.x, 0)].x, spawnPoint.y, 0);
        //enemy.gameObject.SetActive(true);
        //точку спавна
        return enemy;
    }
    
    public void CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector, GameObject player)
    {
        IGridGenerator gridGenerator = new GridGeneratorStandart(_assetProvider);

        gridGenerator.BuildGrid(scaleVector, levelStaticData.GameGridData.GridHeight, levelStaticData.GameGridData.GridWidth, new List<Vector2>(levelStaticData.GameGridData.BlocksCoords), player, levelStaticData.GameGridData.CellSpace);
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

    private void AddScaleVector(Vector3 scaleVector)
    {
        if(scaleVector != Vector3.zero)
        {
            _scaleVector = scaleVector;
        }
        else
        {
            Debug.Log("========== scaleVector is zero");
        }
        
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
