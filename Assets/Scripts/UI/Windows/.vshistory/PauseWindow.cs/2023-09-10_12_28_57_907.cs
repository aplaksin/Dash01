using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : WindowBase
{




    public void Construct()
    {
        PauseGame();
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
