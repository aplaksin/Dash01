using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService : IService
{
    void Construct(LevelSoundData soundData, AudioSource musicSource, AudioSource fxSource);
    void PlaySoundByType();
    void PlayMusicByType();
    void StopMusicByType();
}
