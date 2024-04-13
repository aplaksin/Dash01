using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void IsPlayerAuthorisedExternal();
    
    [DllImport("__Internal")]
    public static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    public static extern void SetPlayerBestScoreExternal();

    public static void SetIsPlayerAuthorised(int isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised == 0 ? false : true;
    }

    private static void GetPlayerBestScore(int score)
    {
        Game.GameContext.PlayerBestScore = score;
    }


}
