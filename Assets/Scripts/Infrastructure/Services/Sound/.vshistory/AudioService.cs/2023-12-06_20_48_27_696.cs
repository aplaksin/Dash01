using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private AudioSource _musicSource;
    private AudioSource _fxSource;
    private AudioClip _levelMusic;
    private AudioClip _mainMenuMusic;
    private AudioClip _gameOverMusic;
    private IAssetProvider _assetProvider;

    public bool IsSoundOn { get; }

    //private Dictionary<SoundType, AudioClip> _audioClipsByType = new Dictionary<SoundType, AudioClip>();
    public AudioService(AudioSource musicSource, AudioSource fxSource, IAssetProvider assetProvider)
    {
        _musicSource = musicSource;
        _fxSource = fxSource;
        _assetProvider = assetProvider;
        _mainMenuMusic = _assetProvider.GetAudioClip(AssetPath.MainMenuMusicPath);
        _gameOverMusic = _assetProvider.GetAudioClip(AssetPath.GameOverMusicPath);
       
    }

    public void Construct(AudioClip levelMusic)
    {
        _levelMusic = levelMusic;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    public void PlayLevelMusic()
    {
        _musicSource.clip = _levelMusic;
        _musicSource.Play();
    }

    public void PlayMainMenuMusic()
    {
        _musicSource.clip = _mainMenuMusic;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        _fxSource.clip = sfxClip;
        _fxSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Pause();
    }

    public void MuteMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public void MuteSFX()
    {
        _fxSource.mute = !_fxSource.mute;
    }

    public void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        _fxSource.volume = value;
    }

    public void PlayGameOverMusic()
    {
        _musicSource.clip = _gameOverMusic;
        _musicSource.Play();
    }
}
