using System.Threading.Tasks;

using UnityEngine;


  public class UIFactory : IUIFactory
  {

    private readonly IStaticDataService _staticData;
    private Game _game;



    public UIFactory(IStaticDataService staticData)
    {
        _staticData = staticData;
        _game = game;
    }

    public void CreatePauseMenu(GameStateMachine gameStateMachine)
    {
        WindowConfig config = _staticData.GetWndowConfigById(WindowId.Pause);
        PauseWindow window = Object.Instantiate(config.Template) as PauseWindow;
        window.Construct(gameStateMachine);
    }

    public void CreateGameOverMenu(GameStateMachine gameStateMachine, int score)
    {
        WindowConfig config = _staticData.GetWndowConfigById(WindowId.GameOver);
        GameOverMenu window = Object.Instantiate(config.Template) as GameOverMenu;
        window.Construct(gameStateMachine, score);
    }

    /*    public async Task CreateUIRoot()
        {
            GameObject root = await _assets.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }*/
}
