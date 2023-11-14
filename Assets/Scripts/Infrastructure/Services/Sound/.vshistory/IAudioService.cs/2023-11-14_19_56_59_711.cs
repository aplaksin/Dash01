using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService : IService
{
    void Construct(Sound[] sounds);
    void PlaySFX(SoundType type);
    void PlayMusicByType(SoundType type);
    void StopMusic();

    void MuteMusic(bool shouldMute);
    void MuteFx(bool shouldMute);

    void ChangeMusicVolume(float value);
    void ChangeFxVolume(float value);
}
