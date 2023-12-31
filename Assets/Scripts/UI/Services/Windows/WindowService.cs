﻿

public class WindowService : IWindowService
{
    private readonly IUIFactory _uiFactory;
    private readonly GameStateMachine _gameStateMachine;
    private GameContext _gameContext;
    private IAudioService _audioService;

    public WindowService(IUIFactory uiFactory, GameStateMachine gameStateMachine, IAudioService audioService)
    {
        _uiFactory = uiFactory;
        _gameStateMachine = gameStateMachine;
        _audioService = audioService;
    }

    public void OpenWindowById(WindowId windowId)
    {
        switch (windowId)
        {
            case WindowId.None:
                break;
            case WindowId.Pause:
                _uiFactory.CreatePauseMenu(_gameStateMachine, _audioService);
                break;
            case WindowId.GameOver:
                _uiFactory.CreateGameOverMenu(_gameStateMachine, Game.GameContext.Score);
                break;
        }
    }

}
