using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    public LoadingScreen loadingCurtain;

    [SerializeField]
    private AudioSource _musicSource;

    [SerializeField]
    private AudioSource _fxSource;
    private Game _game;

    private void Awake()
    {      
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(loadingCurtain.gameObject);
        _game = new Game(this, loadingCurtain, _musicSource, _fxSource);
        _game.GameStateMachine.Enter<BootstrapState>();


    }

}
