using UnityEngine;

public class BootstrapState : IState
{
    private const string MAIN_MENU_SCENE_NAME = "MainMenu";
    private readonly GameStateMachine _gameStateMachine;
    private readonly AllServices _services;
    private AudioSource _musicSource;
    private AudioSource _fxSource;

    public BootstrapState(GameStateMachine gameStateMashine, AllServices services, AudioSource musicSource, AudioSource fxSource)
    {
        _gameStateMachine = gameStateMashine;
        _services = services;
        _musicSource = musicSource;
        _fxSource = fxSource;
        RegisterServices();

    }

    public void Enter()
    {
        EnterMainMenuScene();
    }



    public void Exit()
    {

    }

    private void RegisterServices()
    {


        _services.RegisterSingle<IInputService>(Inputservice());

        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        
        RegisterStaticDataService();
        _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IStaticDataService>()));
        _services.RegisterSingle<IAudioService>(new AudioService(_musicSource, _fxSource, _services.Single<IAssetProvider>()));
        _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>(), _gameStateMachine, _services.Single<IAudioService>()));
        
        _services.RegisterSingle<IPoolingService>(new PoolingService(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>(), _services.Single<IAudioService>()));
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IPoolingService>(), _services.Single<IInputService>()));

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

    private IInputService Inputservice()
    {
        if (Application.isEditor || SystemInfo.deviceType == DeviceType.Desktop)
            return new KeyboardInputService();
        else
            return new SwipeInputService();
    }
}
