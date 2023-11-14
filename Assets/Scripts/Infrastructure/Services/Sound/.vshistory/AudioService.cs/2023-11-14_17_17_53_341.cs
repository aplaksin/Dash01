using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private AudioSource _musicSource;
    private AudioSource _fxSource;
    private Dictionary<SoundType, AudioClip> _audioClipsByType = new Dictionary<SoundType, AudioClip>();
    public AudioService(AudioSource musicSource, AudioSource fxSource)
    {
        _musicSource = musicSource;
        _fxSource = fxSource;
    }

    public void Construct(Sound[] sounds)
    {
        _audioClipsByType.Clear();
        foreach (Sound sound in sounds)
        {
            _audioClipsByType.Add(sound.Type, sound.Clip);
        }
    }

    public void PlayMusicByType(SoundType type)
    {
        if (_audioClipsByType.ContainsKey(type))
        {
            _musicSource.clip = _audioClipsByType[type];
            _musicSource.Play();
        }
    }

    public void PlayFxByType(SoundType type)
    {
        if (_audioClipsByType.ContainsKey(type))
        {
            _fxSource.clip = _audioClipsByType[type];
            _fxSource.Play();
        }
            
    }

    public void StopMusic()
    {
        _musicSource.Pause();
    }

    public void MuteMusic(bool shouldMute)
    {
        _musicSource.mute = shouldMute;
    }

    public void MuteFx(bool shouldMute)
    {
        _fxSource.mute = shouldMute;
    }

    public void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }

    public void ChangeFxVolume(float value)
    {
        _fxSource.volume = value;
    }
}
