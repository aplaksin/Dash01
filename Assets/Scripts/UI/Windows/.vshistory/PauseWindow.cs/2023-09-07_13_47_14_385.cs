using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : WindowBase
{




    public void Construct()
    {
        //gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnPauseBtnClick()
    {
        //Debug.Log("OnPauseBtnClick");
        //UIEventManager.CallOnClickPauseBtnEvent();
    }

    private void Cleanup()
    {
        Time.timeScale = 1.0f;
    }

}
