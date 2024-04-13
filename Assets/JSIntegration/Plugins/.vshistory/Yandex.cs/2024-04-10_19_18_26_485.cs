using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void IsPlayerAuthorisedExternal();
    
    [DllImport("__Internal")]
    public static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    public static extern void SetPlayerBestScoreExternal(int score);

    private void Start()
    {
        EventManager.OnLevelLoaded+= Setu
        EventManager.OnGameOver += OnGameOverForYandex;
    }

    private void OnDestroy()
    {
        EventManager.OnGameOver -= OnGameOverForYandex;
    }

    public static void SetIsPlayerAuthorised(int isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised == 0 ? false : true;
    }

    private static void GetPlayerBestScore(int score)
    {
        Game.GameContext.PlayerBestScore = score;
    }

    private void OnGameOverForYandex()
    {
        TryToSaveBestScoreYandex();
    }

    private void TryToSaveBestScoreYandex()
    {
        if (Game.GameContext.IsPlayerAuthorised)
        {
            if (Game.GameContext.Score > Game.GameContext.PlayerBestScore)
            {
                Yandex.SetPlayerBestScoreExternal(Game.GameContext.Score);
            }
        }
    }

    private void SetupYandex()
    {
        Yandex.IsPlayerAuthorisedExternal();

        if (Game.GameContext.IsPlayerAuthorised)
        {
            Yandex.GetPlayerBestScoreExternal();
        }
    }

}
