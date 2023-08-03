using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "StaticData/Window static data", fileName = "WindowStaticData")]
public class WindowStaticData : ScriptableObject
{
    public List<WindowConfig> Configs;
}
