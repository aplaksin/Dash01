
public interface IUIFactory : IService
{
    void CreatePauseMenu(GameStateMachine gameStateMachine);
    void CreateGameOverMenu(GameStateMachine gameStateMachine, int score);
    /*Task CreateUIRoot();*/
}