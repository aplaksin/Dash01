using System;
using System.Collections;
using System.Runtime.InteropServices;
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


    private bool _canShowAD = true;
    private float _currentTimeScale = 1f;
    private void Start()
    {
        ShowAdFullScreen();// FOR YA  
        EventManager.OnGameLevelLoaded += SetupYandex;
        EventManager.OnShowFullScreenAD += ShowAdFullScreen;// FOR YA
        EventManager.OnGameOver += OnGameOverForYandex;
    }

    private void OnDestroy()
    {
        EventManager.OnGameLevelLoaded -= SetupYandex;
        EventManager.OnShowFullScreenAD -= ShowAdFullScreen;
        EventManager.OnGameOver -= OnGameOverForYandex;
    }

    private void SetIsPlayerAuthorised(string isAuthorised)
    {
        Game.GameContext.IsPlayerAuthorised = Int32.Parse(isAuthorised) == 0 ? false : true;
        
        if (Game.GameContext.IsPlayerAuthorised)
        {
            try
            {
                GetPlayerBestScoreExternal();
            }
            catch(Exception ex)
            {
                Debug.Log("ex===============");
                Debug.Log(ex.Message);
            }
            
        }
        else
        {
            LoadPlayerPrefs();
        }
    }

    private void GetPlayerBestScore(string score)
    {
        //Debug.Log("GetPlayerBestScore " + score);
        int bestScore = 0;
        try
        {
            bestScore = Int32.Parse(score);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
        Game.GameContext.PlayerBestScore = bestScore / Game.GameContext.ScoreUIMultiplier;

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

        if (Game.GameContext.IsBestScoreChanged) { 
            if (Game.GameContext.IsPlayerAuthorised)
            {
                int scoreForSave = Game.GameContext.Score * Game.GameContext.ScoreUIMultiplier;
                SetPlayerBestScoreExternal(scoreForSave);
            }
            else
            {
                SavePlayerPrefs();//TODO переделать
            }
        }

    }

    private void SetupYandex()
    {
        IsPlayerAuthorisedExternal(); // FOR YA
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("score", Game.GameContext.Score * Game.GameContext.ScoreUIMultiplier);
    }

    private void LoadPlayerPrefs()
    {
        int score = PlayerPrefs.GetInt("score", 0);
        Game.GameContext.PlayerBestScore = score / Game.GameContext.ScoreUIMultiplier;
    }

    private void ShowAdFullScreen()
    {
        if(_canShowAD)
        {
            _canShowAD = false;
            StartCoroutine(ResetAdRestriction());
            _currentTimeScale = Time.timeScale;
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
        Time.timeScale = _currentTimeScale;
        ReturnSoundState();
    }

    private IEnumerator ResetAdRestriction()
    {
        yield return new WaitForSeconds(70);
        _canShowAD = true;
    }

}
