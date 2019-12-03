using System;
using System.Collections.Generic;
using Zenject;

public class ChangeVolumeCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    private readonly Dictionary<SoundType, int> Volumes = new Dictionary<SoundType, int>()
    {
        { SoundType.Master, Constants.DefaultMasterVolume },
        { SoundType.Music, Constants.DefaultMusicVolume },
        { SoundType.SFX, Constants.DefaultSFXVolume }
    };

    private SoundType _modifiedSoundType;
    
    public ChangeVolumeCommand SetVolume(SoundType soundType, int volume)
    {
        if (!Volumes.ContainsKey(soundType))
        {
            throw new Exception($"SoundType {soundType} needs to be added to {GetType()}.Volumes!");
        }
        Volumes[soundType] = volume;
        _modifiedSoundType = soundType;
        return Create();
    }

    public ChangeVolumeCommand Create()
    {
        return new ChangeVolumeCommand(Volumes[_modifiedSoundType], _modifiedSoundType, _settingsContainer);
    }
}