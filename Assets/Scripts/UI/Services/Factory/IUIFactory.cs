﻿
public interface IUIFactory : IService
{
    void CreatePauseMenu(GameStateMachine gameStateMachine, IAudioService audioService);
    void CreateGameOverMenu(GameStateMachine gameStateMachine, int score);
    /*Task CreateUIRoot();*/
}