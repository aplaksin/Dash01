using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService : IService
{
    void Construct(LevelSoundData soundData, AudioSource musicSource, AudioSource fxSource);
    void PlayFxByType(SoundType type);
    void PlayMusicByType(SoundType type);
    void StopMusic();
}
