using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void IsAuthorised();
    
    [DllImport("__Internal")]
    private static extern void GetScore();

    [DllImport("__Internal")]
    private static extern void SetScore();

    public static void SetIsPlayerAuthorised(bool isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised;
    }
}
