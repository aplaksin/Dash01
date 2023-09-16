using UnityEngine;

public class Game
{
    public GameStateMachine GameStateMachine;
    //public static LevelStaticData CurrentLevelStaticData;
    public static GameContext GameContext;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
        GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container, coroutineRunner);

    }


}
