using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : IParameterizedState<string>
{
    private GameStateMachine _gameStateMachine;
    private SceneLoader _sceneLoader;
    private IAudioService _audioService;
    private const string LEVEL_SCENE_NAME = "Level1";
    public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IAudioService audioService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _audioService = audioService;
    }

    public void Enter(string param)
    {
        _audioService.PlayMainMenuMusic();
        _sceneLoader.LoadScene(param);

        SubscribeOnMenuEvents();
    }

    public void Exit()
    {
        //IAudioService audioService = AllServices.Container.Single<IAudioService>();
        //audioService.StopMusic();
        UnsubscribeOnMenuEvents();
    }

    private void SubscribeOnMenuEvents()
    {
        UIEventManager.OnClickStartBtn += EnterLoadLavel;
        UIEventManager.OnClickToggleSoundBtn += ToggleSound;
    }

    private void UnsubscribeOnMenuEvents()
    {
        UIEventManager.OnClickStartBtn -= EnterLoadLavel;
        UIEventManager.OnClickToggleSoundBtn -= ToggleSound;
    }

    private void ToggleSound()
    {
        _audioService.MuteMusic();
        _audioService.MuteSFX();
    }

    private void EnterLoadLavel()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(LEVEL_SCENE_NAME);
    }
}
