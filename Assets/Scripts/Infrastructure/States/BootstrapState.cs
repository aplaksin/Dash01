﻿using System;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMashine;
    private readonly SceneLoader _sceneLoader;
    private const string INITIAL_SCENE_NAME = "Initial";
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMashine, SceneLoader sceneLoader, AllServices services)
    {
        _gameStateMashine = gameStateMashine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {

        _sceneLoader.LoadScene(INITIAL_SCENE_NAME, EnterLoadLavel);
    }



    public void Exit()
    {

    }

    private void RegisterServices()
    {


        _services.RegisterSingle<IInputService>(Inputservice());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));

        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));

        RegisterStaticDataService();
    }

    private void RegisterStaticDataService()
    {
        IStaticDataService staticDataService = new StaticDataService();
        staticDataService.Load();
        _services.RegisterSingle<IStaticDataService>(staticDataService);
    }

    private void EnterLoadLavel()
    {
        _gameStateMashine.Enter<LoadLevelState, string>(INITIAL_SCENE_NAME);
    }
    private IInputService Inputservice()
    {
        /*        if (Application.isEditor)
            return new StandaloneInputService();
        else
            return new MobileInputService();*/

        return null;
    }
}
