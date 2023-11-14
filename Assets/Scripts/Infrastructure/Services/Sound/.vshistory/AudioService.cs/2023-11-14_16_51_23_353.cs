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
        _musicSource = musicSource;
        _fxSource = fxSource;
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
