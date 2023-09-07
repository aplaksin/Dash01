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

    public void OnClickStartBtn()
    {
        Debug.Log("OnClickStartBtn");
    }

}
