using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    public void OnPauseBtnClick()
    {
        Debug.Log("OnPauseBtnClick");
        UIEventManager.CallOnClickPauseBtnEvent();
    }
}
