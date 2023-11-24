using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UiMenu : MonoBehaviour
{

    [SerializeField]
    private Button _startBtn;

    [SerializeField]
    private Button _exitBtn;

    [SerializeField]
    private Button _toggleSoundBtn;

    public void OnClickStartBtn()
    {
        UIEventManager.CallOnClickStartBtnEvent();
    }

    public void OnClickToggleSoundBtn()
    {
        UIEventManager.CallOnClickToggleSoundBtnEvent();
    }

    public void OnClickExitBtn()
    {
        //UIEventManager.CallOnClickExitBtnEvent();
        Application.Quit();
    }


}
