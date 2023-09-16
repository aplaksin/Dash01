

public class WindowService : IWindowService
{
    private readonly IUIFactory _uiFactory;
    private readonly GameStateMachine _gameStateMachine;

    public WindowService(IUIFactory uiFactory, GameStateMachine gameStateMachine)
    {
        _uiFactory = uiFactory;
        _gameStateMachine = gameStateMachine;
    }

    public void OpenWindowById(WindowId windowId, int score = -1)
    {
        switch (windowId)
        {
            case WindowId.None:
                break;
            case WindowId.Pause:
                _uiFactory.CreatePauseMenu(_gameStateMachine);
                break;
            case WindowId.GameOver:
                _uiFactory.CreateGameOverMenu(_gameStateMachine, score);
                break;
        }
    }
}
