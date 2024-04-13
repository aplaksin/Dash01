using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void IsPlayerAuthorisedExternal();
    
    [DllImport("__Internal")]
    private static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    private static extern void SetPlayerBestScoreExternal(int score);

    [DllImport("__Internal")]
    private static extern void ShowAdFullScreenExternal();


    private void Start()
    {
        EventManager.OnLevelLoaded += SetupYandex;
        EventManager.OnLevelLoaded += ShowAdFullScreen;
        EventManager.OnGameOver += OnGameOverForYandex;
    }

    private void OnDestroy()
    {
        EventManager.OnLevelLoaded -= SetupYandex;
        EventManager.OnLevelLoaded -= ShowAdFullScreen;
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
                SetPlayerBestScoreExternal(Game.GameContext.Score);
            }
        }
    }

    private void SetupYandex()
    {
        IsPlayerAuthorisedExternal();

        if (Game.GameContext.IsPlayerAuthorised)
        {
            GetPlayerBestScoreExternal();
        }
    }

    private void ShowAdFullScreen()
    {
        Time.timeScale = 0;
        ShowAdFullScreenExternal();
    }

    private void AdFullScreenClosed()
    {
        Time.timeScale = 1;
    }

}
