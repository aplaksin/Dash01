using UnityEngine;

public class Game
{
    public GameStateMachine GameStateMachine;
    public static IInputService InputService;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
        GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);

    }


}
