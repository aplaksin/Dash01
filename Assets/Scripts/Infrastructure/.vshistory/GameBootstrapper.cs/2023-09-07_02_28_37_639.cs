using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain loadingCurtain;
    private Game _game;

    private void Awake()
    {      
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(loadingCurtain.gameObject);
        _game = new Game(this, loadingCurtain);
        _game.GameStateMachine.Enter<BootstrapState>();


    }

}
