using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : WindowBase
{
    private GameStateMachine _gameStateMachine;

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        PauseGame();
        EventManager.OnGameOver += OnGameOver;
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

    protected override void Cleanup()
    {
        ContinueGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    } 
    private void ContinueGame()
    {
        Time.timeScale = 1f;
    }

    private void OnGameOver()
    {
        _closeButton.transform.gameObject.SetActive(false);
    }


}
