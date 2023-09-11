

public class WindowService : IWindowService
{
    private readonly IUIFactory _uiFactory;
    private readonly GameStateMachine _gameStateMachine;

    public WindowService(IUIFactory uiFactory, GameStateMachine gameStateMachine)
    {
        _uiFactory = uiFactory;
        _gameStateMachine = gameStateMachine;
    }

    public void OpenWindowById(WindowId windowId)
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
