using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButton;

    private void OnEnable()
    {
        //_pauseButton.Add
    }

    public void OnPauseBtnClick()
    {
        Debug.Log("OnPauseBtnClick");
        UIEventManager.CallOnClickPauseBtnEvent();
    }
}
