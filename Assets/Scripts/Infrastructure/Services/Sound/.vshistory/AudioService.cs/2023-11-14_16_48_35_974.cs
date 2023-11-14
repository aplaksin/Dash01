using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private LevelSoundData _levelSoundData;
    private AudioSource _musicSource;
    private AudioSource _fxSource;
    public void Construct(LevelSoundData soundData, AudioSource musicSource, AudioSource fxSource)
    {
        _levelSoundData = soundData;
    }

    public void PlayMusicByType()
    {
        throw new System.NotImplementedException();
    }

    public void PlayFxByType()
    {
        throw new System.NotImplementedException();
    }

    public void StopMusicByType()
    {
        throw new System.NotImplementedException();
    }
}
