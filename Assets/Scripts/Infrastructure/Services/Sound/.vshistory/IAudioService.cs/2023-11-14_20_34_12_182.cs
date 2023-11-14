using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService : IService
{
    //void Construct(Sound[] sounds);
    void PlaySFX(AudioClip sfxClip);
    void PlayMusic(AudioClip musicClip);
    void StopMusic();

    void MuteMusic(bool shouldMute);
    void PlayLevelMusic();
    void PlayMainMenuMusic();
    void PlayGameOverMusic();
    void PlayDamagePlayerSFX();
    void MuteSFX(bool shouldMute);

    void ChangeMusicVolume(float value);
    void ChangeSFXVolume(float value);
}
