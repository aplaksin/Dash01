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

    public void OnClickStartBtn()
    {
        UIEventManager.CallOnClickStartBtnEvent();
        Debug.Log("OnClickStartBtn");
    }

    public void OnClickExitBtn()
    {
        //UIEventManager.CallOnClickExitBtnEvent();
        Application.Quit();
    }


}
