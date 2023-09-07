using System;
using UnityEngine;

public class LoadLevelState : IParameterizedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private LoadingCurtain _loadingCurtain;
    private IGameFactory _gameFactory;
    private const string LevelDataName = "Level1";
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private LevelStaticData _levelStaticData;
    private IPoolingService _poolingService;

    public LoadLevelState(GameStateMachine gameStateMachine, IPoolingService poolingService, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _staticDataService = staticDataService;
        _levelStaticData = _staticDataService.GetLevelStaticDataByKey(LevelDataName);
        _poolingService = poolingService;
    }

    public void Enter(string sceneName)
    {
        _loadingCurtain.Show();
        _sceneLoader.LoadScene(sceneName, OnLoaded);
    }
    public void Exit()
    {
        _loadingCurtain.Hide();
    }
    private void OnLoaded()
    {

        _poolingService.Construct();

        CorrectCameraPosition();

        Vector3 scaleVector = CalcScaleVector();
        _gameFactory.Construct(scaleVector);

        Game.CurrentLevelStaticData = _levelStaticData;

        GameObject player = _gameFactory.CreatePlayer(_levelStaticData.PlayerSpawnCoords, scaleVector);
        
        _gameFactory.CreateGameGrid(_levelStaticData, scaleVector, player);

        GameObject hud = _gameFactory.CreateHud();
        hud.GetComponent<PlayerHpUI>().Construct(_levelStaticData.PlayerHP);
        hud.SetActive(true);

        _gameStateMachine.Enter<GameLoopState>();
    }

    private Vector3 CalcScaleVector()
    {
        int resolutionHorizontal = (int)UnityEditor.Handles.GetMainGameViewSize().x;
        int resolutionVertical = (int)UnityEditor.Handles.GetMainGameViewSize().y;

        float cameraSize = Camera.main.orthographicSize * 2;

        float pixelsPerUnit = CalcPixelsPerUnit(resolutionVertical, cameraSize);

        float scaleCoeff = CalcScaleCoefficient(resolutionHorizontal, _levelStaticData.GameGridData.GridWidth, pixelsPerUnit, _levelStaticData.GameGridData.CellSpace);

        return new Vector3(scaleCoeff, scaleCoeff, scaleCoeff);
    }

    private float CalcPixelsPerUnit(int resolutionVertical, float cameraSize)
    {
        return resolutionVertical / cameraSize;
    }

    private float CalcScaleCoefficient(int resolutionHorisontal, int gridWidth, float pixelsPerUnit)
    {
        return (resolutionHorisontal / gridWidth) / pixelsPerUnit;
    }
    private float CalcScaleCoefficient(int resolutionHorisontal, int gridWidth, float pixelsPerUnit, float cellSpace)
    {
        return ((resolutionHorisontal - ((gridWidth - 1) * cellSpace * pixelsPerUnit)) / gridWidth) / pixelsPerUnit;
    }

    private void CorrectCameraPosition()
    {
        float aspectRatio = Camera.main.aspect; 
        float camSize = Camera.main.orthographicSize; 
        float correctPositionX = aspectRatio * camSize;
        Camera.main.transform.position = new Vector3(correctPositionX, camSize, -10);
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.PlayerProgress);
    }


}
