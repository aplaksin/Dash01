using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameFactory : IGameFactory
{

    public Dictionary<Vector2, Vector3> BlocksCoords { get { return _blocksCoords; } }

    private readonly IAssetProvider _assetProvider;
    private readonly IPoolingService _poolingService;

    private readonly IInputService _inputService;


    private Vector3 _scaleVector;
    private Dictionary<Vector2, Vector3> _cellPositionByCoords;
    private Dictionary<Vector2, GameObject> _blocksByCoords;
    private Dictionary<Vector2,Vector3> _blocksCoords;

    private float _scaleCoeffForTanks = 1.4f;
    private float _skinCoeffForPlayer = 1.6f;
    private float _skinCoeffForEnemy = 1.5f;

    public GameFactory(IAssetProvider assetProvider, IPoolingService poolingService, IInputService inputInputService)
    {
        _assetProvider = assetProvider;
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

    public Projectile CreateProjectile(Vector2 spawnPoint)
    {
        Projectile projectile = _poolingService.GetProjectileByType(ProjectileType.Base);
        projectile.transform.position = spawnPoint;
        return projectile;
    }

    public Enemy CreateEnemy(Vector2 spawnPoint, EnemyType enemyType, GameStageStaticData stage)
    {
        Enemy enemy = _poolingService.GetEnemyByType(enemyType);
        enemy.InitProperties(stage);
        Vector3 scale = _scaleVector * _skinCoeffForEnemy;
        if (EnemyType.Tank == enemyType)
        {
            enemy.transform.localScale = scale * _scaleCoeffForTanks;
        }
        else
        {
            enemy.transform.localScale = scale;
        }

        enemy.transform.position = new Vector3(_cellPositionByCoords[new Vector2(spawnPoint.x, 0)].x, spawnPoint.y, 0);

        SelectBehaviourByType(enemyType, enemy);
        
        return enemy;
    }
    
    public Dictionary<Vector2, Vector3> CreateGameGrid(LevelStaticData levelStaticData, Vector3 scaleVector)
    {
        GameGridStaticData gameGridStaticData = SelectGameGridStaticData(levelStaticData);

        IGridGenerator gridGenerator = new GridGeneratorStandart(_assetProvider, gameGridStaticData.GridHeight, gameGridStaticData.GridWidth, gameGridStaticData.GridPadding, scaleVector);

        gridGenerator.BuildGrid(new List<Vector2>(gameGridStaticData.BlocksCoords), _cellPositionByCoords, _blocksByCoords, _blocksCoords, gameGridStaticData.CellSpace);

        return _cellPositionByCoords;
    }

    public void SetPlayerPositionOnGrid(GameObject player, Vector3 spawnPoint, Dictionary<Vector2, Vector3> cellpositionsByCoords)
    {
        player.transform.position = cellpositionsByCoords[spawnPoint];
    }
    
    public GameObject CreatePlayer(Vector2 spawnPoint, Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords, GameContext gameContext)
    {
        GameObject player = _assetProvider.Instantiate(AssetPath.PlayerPath);
        player.transform.localScale = scaleVector * _skinCoeffForPlayer;
        SetPlayerPositionOnGrid(player, spawnPoint, cellpositionsByCoords);
        //player.transform.position = new Vector3(spawnPoint.x + player.transform.localScale.x / 2, spawnPoint.y + player.transform.localScale.y / 2, 0);
        
        PlayerMove playerMove = player.transform.GetComponent<PlayerMove>();

        playerMove.Init(cellpositionsByCoords, _blocksByCoords, spawnPoint, _inputService, this, gameContext);
        
        return player;
    }

    public GameObject CreateHud()
    {
        return _assetProvider.Instantiate(AssetPath.HudPath);
    } 

    public GameObject CreateDamageBorder()
    {
        return _assetProvider.Instantiate(AssetPath.DamageBorderPath);
    }

    public GameObject CreatePauseMenu()
    {
        return _assetProvider.Instantiate(AssetPath.PauseMenuPath);
    }

    public GameObject CreateTutorial()
    {
        return _assetProvider.Instantiate(AssetPath.UITutorial);
    }

    private void SelectBehaviourByType(EnemyType enemyType, Enemy enemy)
    {
        List<Vector3> list = _cellPositionByCoords.Values.ToList();
        switch (enemyType)
        {
            case EnemyType.ZigZag:
                //TODO fix List<Vector3> list = _cellPositionByCoords.Values.ToList();

                enemy.EnemyBeheviour = new ZigzagEnemyBehaviour(enemy.transform, list[0], list[list.Count - 1], enemy._moveSpeed, enemy);
                break;
            case EnemyType.SpeedBufferHorizontal:

                //enemy.BuffsList.Add(new EnemySpeedBuff(3f));
                enemy.EnemyBeheviour = new BufferSpeedHorizontal(enemy.transform, list[0], list[list.Count - 1], enemy._moveSpeed, 9, enemy);
                Game.GameContext.AddEnemyBuff(enemy.BuffsList);
                //Game.GameContext.ApplyEnemyBuffs();
                break;
            case EnemyType.Base:
            case EnemyType.Tank:
                enemy.EnemyBeheviour = new MoveDownBeheviour(enemy.transform, enemy.MoveSpeed, enemy);
                break;
            default:
                enemy.EnemyBeheviour = new MoveDownBeheviour(enemy.transform, enemy.MoveSpeed, enemy);
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

    private GameGridStaticData SelectGameGridStaticData(LevelStaticData levelStaticData)
    {
        return levelStaticData.GameGridStaticDataList.Count > 0 ? levelStaticData.GameGridStaticDataList[Random.Range(0, levelStaticData.GameGridStaticDataList.Count)] : levelStaticData.GameGridData;
    }

    public void Cleanup()
    {

    }

    



}
