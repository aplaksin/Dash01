using System.Threading.Tasks;

using UnityEngine;


  public class UIFactory : IUIFactory
  {
    //private const string UIRootPath = "UIRoot";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    
    private Transform _uiRoot;


    public UIFactory(IAssetProvider assets, IStaticDataService staticData)
    {
      _assets = assets;
      _staticData = staticData;
    }

    public void CreatePauseMenu()
    {
      WindowConfig config = _staticData.GetWndowConfigById(WindowId.Pause);
      TestWindow window = Object.Instantiate(config.Template, _uiRoot) as TestWindow;
      window.Construct();
    }

/*    public async Task CreateUIRoot()
    {
        GameObject root = await _assets.Instantiate(UIRootPath);
        _uiRoot = root.transform;
    }*/
  }
