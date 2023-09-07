using System;


public static class UIEventManager
{
    public static event Action OnClickStartBtn;
    public static event Action OnClickResumeBtn;
    public static event Action OnClickRestartBtn;
    public static event Action OnClickExitBtn;

    public static void CallOnClickStartBtnEvent()
    {
        OnClickStartBtn?.Invoke();
    }
    public static void CallOnClickResumeBtnEvent()
    {
        OnClickResumeBtn?.Invoke();
    }
    public static void CallOnClickRestartBtnEvent()
    {
        OnClickRestartBtn?.Invoke();
    }
    public static void CallOnClickExitBtnEvent()
    {
        OnClickExitBtn?.Invoke();
    }

}
