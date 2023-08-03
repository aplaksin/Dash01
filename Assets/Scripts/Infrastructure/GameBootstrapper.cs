using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain loadingCurtain;
    private Game _game;

    private void Awake()
    {
        _game = new Game(this, loadingCurtain);
        _game.GameStateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
