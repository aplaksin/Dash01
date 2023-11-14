using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService : IService
{
    void PlaySoundByType();
    void PlayMusicByType();
    void StopMusicByType();
}
