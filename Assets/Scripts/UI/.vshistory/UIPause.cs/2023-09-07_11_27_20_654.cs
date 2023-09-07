using UnityEngine;


public class UIPause : MonoBehaviour
{
    public void OnPauseBtnClick()
    {
        Debug.Log("OnPauseBtnClick");
        UIEventManager.CallOnClickPauseBtnEvent();
    }
}
