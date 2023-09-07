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
    private Button _restartBtn;
    [SerializeField]
    private Button _resumeBtn;
    [SerializeField]
    private Button _exitBtn;

    private void OnClickStartBtn()
    {
        UIEventManager.CallOnClickStartBtnEvent();
    }
    private void OnClickRestartBtn()
    {
        UIEventManager.CallOnClickRestartBtnEvent();
    }
    private void OnClickResumeBtn()
    {
        UIEventManager.CallOnClickResumeBtnEvent();
    }
    private void OnClickExitBtn()
    {
        UIEventManager.CallOnClickExitBtnEvent();
    }


}
