using System;
using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    //private const string INITIAL_SCENE_NAME = "Initial";
    
    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMashine, SceneLoader sceneLoader, AllServices services)
    {
        _gameStateMachine = gameStateMashine;
        //_sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {

        //_sceneLoader.LoadScene(INITIAL_SCENE_NAME, EnterMainMenuScene);
        EnterMainMenuScene();
    }



    public void Exit()
    {

    }

    private void RegisterServices()
    {


        _services.RegisterSingle<IInputService>(Inputservice());
        //_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        
        RegisterStaticDataService();
        _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IStaticDataService>()));
        _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>(), _gameStateMachine));
        
        _services.RegisterSingle<IPoolingService>(new PoolingService(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>()));
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>(), _services.Single<IPoolingService>(), _services.Single<IInputService>()));
        _services.RegisterSingle<IAudioService>(new AudioService(Game.MusicSource, Game.FxSource));
        Debug.Log(Game.MusicSource);
        //_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));

        
    }

    private void RegisterStaticDataService()
    {
        IStaticDataService staticDataService = new StaticDataService();
        staticDataService.Load();
        _services.RegisterSingle<IStaticDataService>(staticDataService);
    }

    private void EnterMainMenuScene()
    {
        _gameStateMachine.Enter<MainMenuState, string>(MAIN_MENU_SCENE_NAME);
    }

    /*    private void EnterLoadLavel()
        {
            _gameStateMashine.Enter<LoadLevelState, string>(LEVEL_SCENE_NAME);
        }*/
    private IInputService Inputservice()
    {
        if (Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop)
            return new KeyboardInputManager();
        else
            return new SwipeInputManager();
    }
}
