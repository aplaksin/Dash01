using System;
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
        //ShowAdFullScreen();
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

    private void SetIsPlayerAuthorised(string isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = Int32.Parse(isAuthorised) == 0 ? false : true;
        Debug.Log("IsPlayerAuthorised " + Game.GameContext.IsPlayerAuthorised);
    }

    private void GetPlayerBestScore(string score)
    {
        int bestScore = Int32.Parse(score);
        Game.GameContext.PlayerBestScore = bestScore / Game.GameContext.ScoreUIMultiplier;
        Debug.Log("score " + score);
        if (bestScore != 0)
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
                SetPlayerBestScoreExternal(Game.GameContext.Score * Game.GameContext.ScoreUIMultiplier);
            }
        }
        else
        {
            SaveScoreLocalExternal(Game.GameContext.Score * Game.GameContext.ScoreUIMultiplier);
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
            MuteAllSound();
            ShowAdFullScreenExternal();
        }

    }

    private void MuteAllSound()
    {
        _currentMusicSourceActiveState = _musicSource.mute;
        _musicSource.mute = true;

        _currentSfxSourceActivestate = _sfxSource.mute;
        _sfxSource.mute = true;
    }

    private void ReturnSoundState()
    {
        _musicSource.mute = _currentMusicSourceActiveState;
        _sfxSource.mute = _currentSfxSourceActivestate;
    }

    private void LoadScoreLocal(string score)
    {
        int localScore = Int32.Parse(score) / Game.GameContext.ScoreUIMultiplier;
        Game.GameContext.PlayerBestScore = localScore;
    }

    private void AdFullScreenClosed()
    {
        Debug.Log("AdFullScreenClosed");
        Time.timeScale = Game.GameContext.CurrentTimeScale;
        ReturnSoundState();
    }

    private IEnumerator ResetAdRestriction()
    {
        yield return new WaitForSeconds(70);
        _canShowAD = true;
    }

}
