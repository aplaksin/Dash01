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
    private readonly IInputService _inputService;
    private LevelStaticData levelStaticData;

    private Vector3 _scaleVector;
    private Dictionary<Vector2, Vector3> _cellPositionByCoords;
    private Dictionary<Vector2, GameObject> _blocksByCoords;
    private Dictionary<Vector2,Vector3> _blocksCoords;

    public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPoolingService poolingService, IInputService inputInputService)
    {
        _assetProvider = assetProvider;
        _staticDataService = staticDataService;
        _poolingService = poolingService;
        _inputService = inputInputService;
    }



    public void Construct(Vector3 scaleVector)
    {
        _cellPositionByCoords = new Dictionary<Vector2, Vector3>();
        _blocksByCoords = new Dictionary<Vector2, GameObject>();
        _blocksCoords = new Dictionary<Vector2, Vector3>();
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
    
    public Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector)
    {
        IGridGenerator gridGenerator = new GridGeneratorStandart(_assetProvider, levelStaticData.GameGridData.GridHeight, levelStaticData.GameGridData.GridWidth, scaleVector);

        gridGenerator.BuildGrid(new List<Vector2>(levelStaticData.GameGridData.BlocksCoords), _cellPositionByCoords, _blocksByCoords, _blocksCoords, levelStaticData.GameGridData.CellSpace);

        return _cellPositionByCoords;
    }
    public void SetPlayerPositionOnGrid(GameObject player, Vector3 spawnPoint, Dictionary<Vector2, Vector3> cellpositionsByCoords)
    {
        player.transform.position = cellpositionsByCoords[spawnPoint];
    }
    
    public GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords)
    {
        GameObject player = _assetProvider.Instantiate(AssetPath.PlayerPath);
        player.transform.localScale = scaleVector;
        SetPlayerPositionOnGrid(player, spawnPoint, cellpositionsByCoords);
        //player.transform.position = new Vector3(spawnPoint.x + player.transform.localScale.x / 2, spawnPoint.y + player.transform.localScale.y / 2, 0);
        
        PlayerMove playerMove = player.transform.GetComponent<PlayerMove>();

        playerMove.Init(cellpositionsByCoords, _blocksByCoords, spawnPoint, _inputService, this);
        
        return player;
    }

    public GameObject CreateHud()
    {
        return _assetProvider.Instantiate(AssetPath.HudPath);
    } 
    public GameObject CreatePauseMenu()
    {
        return _assetProvider.Instantiate(AssetPath.PauseMenuPath);
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
