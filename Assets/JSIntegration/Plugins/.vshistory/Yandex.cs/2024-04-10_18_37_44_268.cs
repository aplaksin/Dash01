using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void IsPlayerAuthorisedExternal();
    
    [DllImport("__Internal")]
    private static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    private static extern void SetPlayerBestScoreExternal();

    public static void SetIsPlayerAuthorised(int isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised == 0 ? false : true;
    }

    public static void SetPlayerBestScore(int score)
    {
        Game.GameContext.PlayerBestScore = score;
    }


}
