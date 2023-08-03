public interface IState : IExitableState
{
    void Enter();
    
}

public interface IExitableState
{
    void Exit();
}

public interface IParameterizedState<TParam> : IExitableState
{

    void Enter(TParam param);

}