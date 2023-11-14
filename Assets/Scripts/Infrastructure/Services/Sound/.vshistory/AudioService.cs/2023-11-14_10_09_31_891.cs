using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private IAssetProvider _assetProvider;
    public AudioService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
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
