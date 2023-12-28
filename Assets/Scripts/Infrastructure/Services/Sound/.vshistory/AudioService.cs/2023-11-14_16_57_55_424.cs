using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private LevelSoundData _levelSoundData;
    private AudioSource _musicSource;
    private AudioSource _fxSource;
    private Dictionary<SoundType, AudioClip> _audioClips = new Dictionary<SoundType, AudioClip>();
    public AudioService(AudioSource musicSource, AudioSource fxSource)
    {
        _musicSource = musicSource;
        _fxSource = fxSource;
    }

    public void Construct(Sound[] sounds)
    {

    }

    public void PlayMusicByType(SoundType type)
    {
        throw new System.NotImplementedException();
    }

    public void PlayFxByType(SoundType type)
    {
        throw new System.NotImplementedException();
    }

    public void StopMusic()
    {
        throw new System.NotImplementedException();
    }
}