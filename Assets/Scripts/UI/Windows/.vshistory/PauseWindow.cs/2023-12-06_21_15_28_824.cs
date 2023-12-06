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
        PrepareToggleSoundButton();


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

    public void OnPauseBtnClick()
    {
        //Debug.Log("OnPauseBtnClick");
        //UIEventManager.CallOnClickPauseBtnEvent();
    }

    public void OnSoundTurn()
    {   //TODO rename MUTE to TOGGLE
        //_audioService.MuteMusic();
        //_audioService.MuteSFX();
        _audioService.ToggleAllSounds();
        Debug.Log("OnSoundTurn");
    }

    protected override void Cleanup()
    {
        ContinueGame();
    }

    private void PrepareToggleSoundButton()
    {
        _soundToggle.interactable = false;
        _soundToggle.isOn = _audioService.IsSoundOn;
        _soundToggle.interactable = true;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1f;
    }

/*    private void OnGameOver()
    {
        PauseGame();
        _closeButton.transform.gameObject.SetActive(false);
    }*/


}
