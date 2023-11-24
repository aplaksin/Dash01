using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameFactory : IGameFactory
{


    public Dictionary<Vector2, Vector3> BlocksCoords { get { return _blocksCoords; } }

    //private List<Vector2> _blocks = new List<Vector2>();
    private readonly IAssetProvider _assetProvider;
    private readonly IPoolingService _poolingService;
    //private readonly IStaticDataService _staticDataService;
    private readonly IInputService _inputService;
    //private LevelStaticData levelStaticData;

    private Vector3 _scaleVector;
    private Dictionary<Vector2, Vector3> _cellPositionByCoords;
    private Dictionary<Vector2, GameObject> _blocksByCoords;
    private Dictionary<Vector2,Vector3> _blocksCoords;
    //private IAudioService _audioService;
    public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, IPoolingService poolingService, IInputService inputInputService)
    {
        _assetProvider = assetProvider;
        //_staticDataService = staticDataService;
        _poolingService = poolingService;
        _inputService = inputInputService;
    }



    public void Construct(Vector3 scaleVector)
    {
        //_audioService = AllServices.Container.Single<IAudioService>();
        _cellPositionByCoords = new Dictionary<Vector2, Vector3>();
        _blocksByCoords = new Dictionary<Vector2, GameObject>();
        _blocksCoords = new Dictionary<Vector2, Vector3>();
        AddScaleVector(scaleVector);
        //TODO переделать прокидывание scaleVector мб перенести CalcScaleVector() из лоад левел сюда
        // создать метод что-то вроде CreateBaseGameObjects и там создавать грид и плеера
        // возможно стоит выделить отдельно энеми мувер

    }

    public Projectile CreateProjectile(Vector2 spawnPoint)
    {
        Projectile projectile = _poolingService.GetProjectileByType(ProjectileType.Base);
        projectile.transform.position = spawnPoint;
        //_audioService.PlaySFX(SoundType.PlayerShoot);
        return projectile;
    }

    public Enemy CreateEnemy(Vector2 spawnPoint, EnemyType enemyType, GameStageStaticData stage)
    {
        Enemy enemy = _poolingService.GetEnemyByType(enemyType);
        enemy.InitProperties(stage);
        enemy.transform.localScale = _scaleVector;
        enemy.transform.position = new Vector3(_cellPositionByCoords[new Vector2(spawnPoint.x, 0)].x, spawnPoint.y, 0);

        SelectBehaviourByType(enemyType, enemy);

        return enemy;
    }
    
    public Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector)
    {
        IGridGenerator gridGenerator = new GridGeneratorStandart(_assetProvider, levelStaticData.GameGridData.GridHeight, levelStaticData.GameGridData.GridWidth, levelStaticData.GameGridData.GridPadding, scaleVector);

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


    private void SelectBehaviourByType(EnemyType enemyType, Enemy enemy)
    {
        switch (enemyType)
        {
            case EnemyType.ZigZag:
                //TODO fix
                List<Vector3> list = _cellPositionByCoords.Values.ToList();
                enemy.EnemyBeheviour = new ZigzagEnemyBehaviour(enemy.transform, list[0], list[list.Count - 1], enemy._moveSpeed);
                break;
            case EnemyType.Base:
            case EnemyType.Tank:
                enemy.EnemyBeheviour = new MoveDownBeheviour(enemy.transform, enemy.MoveSpeed);
                break;
            default:
                enemy.EnemyBeheviour = new MoveDownBeheviour(enemy.transform, enemy.MoveSpeed);
                break;
        }
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

    public void Cleanup()
    {

    }

    



}
