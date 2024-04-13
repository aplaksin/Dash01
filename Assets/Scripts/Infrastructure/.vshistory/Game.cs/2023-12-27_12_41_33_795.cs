using UnityEngine;

public class Game
{
    public GameStateMachine GameStateMachine;

    //public static LevelStaticData CurrentLevelStaticData;
    public static GameContext GameContext;
    public static bool CanPlayerSwipeDirection = false;
    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingCurtain, AudioSource musicSource, AudioSource fxSource)
    {
        GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container, coroutineRunner, musicSource, fxSource);

    }


}
