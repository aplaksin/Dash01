using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : IParameterizedState<string>
{
    private GameStateMachine _gameStateMachine;
    private SceneLoader _sceneLoader;
    private const string LEVEL_SCENE_NAME = "Level1";
    public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter(string param)
    {
        _sceneLoader.LoadScene(param);
        SubscribeOnMenuEvents();
    }

    public void Exit()
    {
        UnsubscribeOnMenuEvents();
    }

    private void SubscribeOnMenuEvents()
    {
        UIEventManager.OnClickStartBtn += EnterLoadLavel;
    }

    private void UnsubscribeOnMenuEvents()
    {
        UIEventManager.OnClickStartBtn -= EnterLoadLavel;
    }

    private void EnterLoadLavel()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(LEVEL_SCENE_NAME);
    }
}
