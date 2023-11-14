using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private SceneLoader _sceneLoader;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingCurtain, AllServices services, ICoroutineRunner coroutineRunner)
    {

        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader),
            //[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
            [typeof(LoadLevelState)] = new LoadLevelState(this, services.Single<IPoolingService>(), sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<IStaticDataService>(), services.Single<IUIFactory>(), services.Single<IWindowService>(), services.Single<IAudioService>()),
            [typeof(GameLoopState)] = new GameLoopState(services.Single<IGameFactory>(), coroutineRunner, services.Single<IWindowService>())

        };

        _sceneLoader = sceneLoader;
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TParam>(TParam param) where TState : class, IParameterizedState<TParam>
    {
        TState state = ChangeState<TState>();
        state.Enter(param);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();
        TState state = GetState<TState>();
        _activeState = state;
        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
        return _states[typeof(TState)] as TState; //TODO подумать над изменением даункаста
    }


}
