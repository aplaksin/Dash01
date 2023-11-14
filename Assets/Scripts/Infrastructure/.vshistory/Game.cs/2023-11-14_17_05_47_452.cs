using UnityEngine;

public class Game
{
    public GameStateMachine GameStateMachine;
    public static AudioSource MusicSource;
    public static AudioSource FxSource;
    //public static LevelStaticData CurrentLevelStaticData;
    public static GameContext GameContext;

    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingCurtain, AudioSource musicSource, AudioSource fxSource)
    {
        GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container, coroutineRunner);
        MusicSource = musicSource;
        FxSource = fxSource;
    }


}
