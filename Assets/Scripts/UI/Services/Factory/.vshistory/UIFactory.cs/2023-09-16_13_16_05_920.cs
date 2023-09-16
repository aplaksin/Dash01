using System.Threading.Tasks;

using UnityEngine;


  public class UIFactory : IUIFactory
  {
    //private const string UIRootPath = "UIRoot";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    
    //private Transform _uiRoot;


    public UIFactory(IAssetProvider assets, IStaticDataService staticData)
    {
      _assets = assets;
      _staticData = staticData;
    }

    public void CreatePauseMenu(GameStateMachine gameStateMachine)
    {
      WindowConfig config = _staticData.GetWndowConfigById(WindowId.Pause);
      PauseWindow window = Object.Instantiate(config.Template) as PauseWindow;
      window.Construct(gameStateMachine);
    }

    public void CreateGameOverMenu(GameStateMachine gameStateMachine)
    {
        WindowConfig config = _staticData.GetWndowConfigById(WindowId.GameOver);
        GameOverMenu window = Object.Instantiate(config.Template) as GameOverMenu;
        window.Construct(gameStateMachine, Game.GameContext.Score);
    }

    /*    public async Task CreateUIRoot()
        {
            GameObject root = await _assets.Instantiate(UIRootPath);
            _uiRoot = root.transform;
        }*/
}
