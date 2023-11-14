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
        IAudioService audioService = AllServices.Container.Single<IAudioService>();
        audioService.PlayMusicByType(SoundType.MenuMusic);
        _sceneLoader.LoadScene(param);

        SubscribeOnMenuEvents();
    }

    public void Exit()
    {
        IAudioService audioService = AllServices.Container.Single<IAudioService>();
        //audioService.StopMusic();
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
