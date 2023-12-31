﻿

public class WindowService : IWindowService
{
    private readonly IUIFactory _uiFactory;

    public WindowService(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void Open(WindowId windowId)
    {
        switch (windowId)
        {
            case WindowId.None:
                break;
            case WindowId.Pause:
                _uiFactory.CreatePauseMenu();
                break;
        }
    }
}
