using System.Collections;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    private bool _currentMusicSourceActiveState;
    private bool _currentSfxSourceActivestate;

    [DllImport("__Internal")]
    private static extern void IsPlayerAuthorisedExternal();
    
    [DllImport("__Internal")]
    private static extern void GetPlayerBestScoreExternal();

    [DllImport("__Internal")]
    private static extern void SetPlayerBestScoreExternal(int score);

    [DllImport("__Internal")]
    private static extern void ShowAdFullScreenExternal();

    [DllImport("__Internal")]
    private static extern void SaveScoreLocalExternal(int score);

    [DllImport("__Internal")]
    private static extern int LoadScoreLocalExternal();



    private bool _canShowAD = true;

    private void Start()
    {
        ShowAdFullScreen();
        EventManager.OnLevelLoaded += SetupYandex;
        EventManager.OnShowFullScreenAD += ShowAdFullScreen;
        EventManager.OnGameOver += OnGameOverForYandex;
    }

    private void OnDestroy()
    {
        EventManager.OnLevelLoaded -= SetupYandex;
        EventManager.OnShowFullScreenAD -= ShowAdFullScreen;
        EventManager.OnGameOver -= OnGameOverForYandex;
    }

    public static void SetIsPlayerAuthorised(int isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = isAuthorised == 0 ? false : true;
        Debug.Log("IsPlayerAuthorised " + Game.GameContext.IsPlayerAuthorised);
    }

    private static void GetPlayerBestScore(int score)
    {
        Game.GameContext.PlayerBestScore = score;
        Debug.Log("score " + score);
        if (score != 0)
        {
            Game.IsTutorialDone = true;
        }
    }

    private void OnGameOverForYandex()
    {
        TryToSaveBestScoreYandex();
    }

    private void TryToSaveBestScoreYandex()
    {
        Debug.Log("TryToSaveBestScoreYandex");
        if (Game.GameContext.IsPlayerAuthorised)
        {
            if (Game.GameContext.Score > Game.GameContext.PlayerBestScore)
            {
                SetPlayerBestScoreExternal(Game.GameContext.Score);
            }
        }
        else
        {
            SaveScoreLocalExternal(Game.GameContext.Score);
        }
    }

    private void SetupYandex()
    {
        Debug.Log("SetupYandex");
        IsPlayerAuthorisedExternal();

        if (Game.GameContext.IsPlayerAuthorised)
        {
            GetPlayerBestScoreExternal();
        }
        else
        {
            LoadScoreLocalExternal();
        }

    }

    private void ShowAdFullScreen()
    {
        Debug.Log("ShowAdFullScreen");
        if(_canShowAD)
        {
            _canShowAD = false;
            StartCoroutine(ResetAdRestriction());
            Time.timeScale = 0;
            ShowAdFullScreenExternal();
        }

    }

    private void MuteSound()
    {
        _currentMusicSourceActiveState = _musicSource.mute;

    }

    private void LoadScoreLocal(int score)
    {
        Game.GameContext.PlayerBestScore = score;
    }

    private void AdFullScreenClosed()
    {
        Debug.Log("AdFullScreenClosed");
        Time.timeScale = Game.GameContext.CurrentTimeScale;
    }

    private IEnumerator ResetAdRestriction()
    {
        yield return new WaitForSeconds(70);
        _canShowAD = true;
    }

}