using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private LevelSoundData _levelSoundData;

    public void Construct(LevelSoundData soundData)
    {
        _levelSoundData = soundData;
    }

    public void PlayMusicByType()
    {
        throw new System.NotImplementedException();
    }

    public void PlaySoundByType()
    {
        throw new System.NotImplementedException();
    }

    public void StopMusicByType()
    {
        throw new System.NotImplementedException();
    }
}
