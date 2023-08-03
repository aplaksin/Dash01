using UnityEditor;
using UnityEngine;


public class Tools
{
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}

