using System.Collections.Generic;

using UnityEngine;

public class LoadLevelState : IParameterizedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private LoadingScreen _loadingCurtain;
    private IGameFactory _gameFactory;
    //private const string LevelDataName = "Level1";
    private string _levelDataName;
    //private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private LevelStaticData _levelStaticData;
    private readonly IPoolingService _poolingService;
    //private readonly IUIFactory _uiFactory;
    private readonly IWindowService _windowService;
    private GameContext _gameContext;

    public LoadLevelState(GameStateMachine gameStateMachine, IPoolingService poolingService, SceneLoader sceneLoader, LoadingScreen loadingCurtain, IGameFactory gameFactory, IStaticDataService staticDataService, IUIFactory uIFactory, IWindowService windowService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
        _gameFactory = gameFactory;
        _staticDataService = staticDataService;
        //_levelStaticData = _staticDataService.GetLevelStaticDataByKey(LevelDataName);
        _poolingService = poolingService;
        //_uiFactory = uIFactory;
        _windowService = windowService;
    }

    public void Enter(string sceneName)
    {
        _levelDataName = sceneName;
        _levelStaticData = _staticDataService.GetLevelStaticDataByKey(_levelDataName);
        _loadingCurtain.Show();
        _sceneLoader.LoadScene(sceneName, OnLoaded);
    }
    public void Exit()
    {
        _loadingCurtain.Hide();
    }
    private void OnLoaded()
    {
        _gameContext = new GameContext(_levelStaticData);
        Game.GameContext = _gameContext;
        PreparePoolingService();
        CorrectCameraPosition();

        Vector3 scaleVector = CalcScaleVector();
        PrepareGameFactory(scaleVector);

        //AddCurrentLevelStaticDataToGame();
        Dictionary<Vector2, Vector3>  cellpositionsByCoords = CreateGameGrid(scaleVector);
        CreatePlayer(scaleVector, cellpositionsByCoords);
        
        CreateHud();


        //GameObject pauseMenu = 
        //_uiFactory.CreatePauseMenu();
        //pauseMenu.SetActive(false);

        _gameStateMachine.Enter<GameLoopState, LevelStaticData>(_levelStaticData);
    }



    private Dictionary<Vector2, Vector3> CreateGameGrid(Vector3 scaleVector)
    {
        return _gameFactory.CreateGameGrid(_levelStaticData, scaleVector);
    }

    private GameObject CreatePlayer(Vector3 scaleVector, Dictionary<Vector2, Vector3> cellpositionsByCoords)
    {
        return _gameFactory.CreatePlayer(_levelStaticData.PlayerSpawnCoords, scaleVector, cellpositionsByCoords);
    }

    private void PrepareGameFactory(Vector3 scaleVector)
    {
        _gameFactory.Construct(scaleVector);
    }

    private void PreparePoolingService()
    {
        _poolingService.Construct();
    }

/*    private void AddCurrentLevelStaticDataToGame()
    {
        Game.CurrentLevelStaticData = _levelStaticData;
    }*/

    private void CreateHud()
    {
        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponent<UIPlayerHp>().Construct(_levelStaticData.PlayerHP);
        hud.SetActive(true);
        hud.GetComponentInChildren<OpenWindowButton>().Init(_windowService);
    }

    private Vector3 CalcScaleVector()
    {
        //int resolutionHorizontal = (int) UnityEditor.Handles.GetMainGameViewSize().x;
        //int resolutionVertical = (int) UnityEditor.Handles.GetMainGameViewSize().y;
        int resolutionHorizontal = Screen.width;
        int resolutionVertical = Screen.height;

        float cameraSize = Camera.main.orthographicSize * 2;

        float pixelsPerUnit = CalcPixelsPerUnit(resolutionVertical, cameraSize);

        float scaleCoeff = CalcScaleCoefficient(resolutionHorizontal, _levelStaticData.GameGridData.GridWidth, pixelsPerUnit, _levelStaticData.GameGridData.CellSpace, _levelStaticData.GameGridData.GridPadding);

        return new Vector3(scaleCoeff, scaleCoeff, scaleCoeff);
    }

    private float CalcPixelsPerUnit(int resolutionVertical, float cameraSize)
    {
        return resolutionVertical / cameraSize;
    }

/*    private float CalcScaleCoefficient(int resolutionHorisontal, int gridWidth, float pixelsPerUnit)
    {
        return (resolutionHorisontal / gridWidth) / pixelsPerUnit;
    }*/
    private float CalcScaleCoefficient(int resolutionHorisontal, int gridWidth, float pixelsPerUnit, float cellSpace, Vector2 padding)
    {
        return ((resolutionHorisontal - ((gridWidth - 1) * cellSpace * pixelsPerUnit) + 2*padding.x) / gridWidth) / pixelsPerUnit;
    }

    private void CorrectCameraPosition()
    {
        float aspectRatio = Camera.main.aspect; 
        float camSize = Camera.main.orthographicSize; 
        float correctPositionX = aspectRatio * camSize;
        Camera.main.transform.position = new Vector3(correctPositionX, camSize, -10);
    }

/*    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.PlayerProgress);
    }*/


}
