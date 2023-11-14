using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private LevelSoundData _levelSoundData;
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
        
    }

    public void PlayFxByType(SoundType type)
    {
        throw new System.NotImplementedException();
    }

    public void StopMusic()
    {
        throw new System.NotImplementedException();
    }

    public void MuteMusic(bool shouldMute)
    {
        _musicSource.mute = shouldMute;
    }

    public void MuteFx(bool shouldMute)
    {
        _fxSource.mute = shouldMute;
    }
}
