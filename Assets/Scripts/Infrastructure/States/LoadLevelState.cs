using System;
using UnityEngine;

public class LoadLevelState : IParameterizedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private LoadingCurtain _loadingCurtain;
    private IGameFactory _gameFactory;
    private const string InitialPointTag = "InitialPoint";
    private readonly IPersistentProgressService _progressService;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
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
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //var initialPoint = GameObject.FindWithTag(InitialPointTag);
        //GameObject hero = _gameFactory.CreateHero(initialPoint);
        //GameObject hud = _gameFactory.CreateHud();

        //CameraFollow(hero);


        _gameStateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.PlayerProgress);
    }

}
