using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void IsAuthorised();
    
    [DllImport("__Internal")]
    private static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    private static extern void SetPlayerBestScoreExternal();

    public static void SetIsPlayerAuthorised(bool isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised;
    }

    public static void SetPlayerBestScore(int score)
    {
        Game.GameContext.PlayerBestScore = score;
    }
}
