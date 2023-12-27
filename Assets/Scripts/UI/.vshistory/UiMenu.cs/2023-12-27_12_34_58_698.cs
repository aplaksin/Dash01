using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UiMenu : MonoBehaviour
{
    //TODO dele this buttons
    [SerializeField]
    private Button _startBtn;

    [SerializeField]
    private Button _exitBtn;

    [SerializeField]
    private Toggle _toggleSoundBtn;

    public void OnClickStartBtn()
    {
        UIEventManager.CallOnClickStartBtnEvent();
    }

    public void OnClickToggleSoundBtn()
    {
        UIEventManager.CallOnClickToggleSoundBtnEvent();
    }
    public void OnClickToggleClickCanPlayerSwipeDirectionBtn()
    {
        UIEventManager.CallOnClickCanPlayerSwipeDirectionBtnEvent();
    }




    public void OnClickExitBtn()
    {
        //UIEventManager.CallOnClickExitBtnEvent();
        Application.Quit();
    }


}
