public class GameLoopState : IState
{
    private GameStateMachine _gameStateMashine;

    public GameLoopState(GameStateMachine gameStateMashine)
    {
        _gameStateMashine = gameStateMashine;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }
}