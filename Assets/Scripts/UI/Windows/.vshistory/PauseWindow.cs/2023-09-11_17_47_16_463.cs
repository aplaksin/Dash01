using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : WindowBase
{
    private GameStateMachine _gameStateMachine;

    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        PauseGame();
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void OnRestartMenuBtnClick()
    {

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



}
