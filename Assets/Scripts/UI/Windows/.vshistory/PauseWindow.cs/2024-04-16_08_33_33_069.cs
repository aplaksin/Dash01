using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWindow : WindowBase
{
    private GameStateMachine _gameStateMachine;

    [SerializeField]
    private Toggle _soundToggle;

    private IAudioService _audioService;

    public void Construct(GameStateMachine gameStateMachine, IAudioService audioService)
    {
        _gameStateMachine = gameStateMachine;
        _audioService = audioService;
        _soundToggle.SetIsOnWithoutNotify(!_audioService.IsSoundOn);
        //Debug.Log("_audioService.IsSoundOn   " + _audioService.IsSoundOn);

        PauseGame();
        //EventManager.OnGameOver += OnGameOver;
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void OnRestartMenuBtnClick()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
    }

    public void OnToggleCanPlayerSwipeDirection()
    {
        Game.CanPlayerSwipeDirection = !Game.CanPlayerSwipeDirection;
    }

    public void OnPauseBtnClick()
    {
        //TODO check this func
        //Debug.Log("OnPauseBtnClick");
        //UIEventManager.CallOnClickPauseBtnEvent();
    }

    public void OnSoundTurn()
    {   
        _audioService.ToggleAllSounds();
    }

    protected override void Cleanup()
    {
        ContinueGame();
    }

    private void PauseGame()
    {
        Game.GameContext.CurrentTimeScale = 0f;
        Time.timeScale = 0f;
    } 
    private void ContinueGame()
    {
        Game.GameContext.CurrentTimeScale = 1f;
        Time.timeScale = 1f;
    }



}
