using System;
using System.Collections.Generic;

public class ChangeVolumeCommandFactory : CommandFactory
{
    private readonly SettingsContainer _settingsContainer;
    private readonly Dictionary<SoundType, int> Volumes = new Dictionary<SoundType, int>()
    {
        { SoundType.Master, Constants.DefaultMasterVolume },
        { SoundType.Music, Constants.DefaultMusicVolume },
        { SoundType.SFX, Constants.DefaultSFXVolume }
    };
    private SoundType _modifiedSoundType;

    public ChangeVolumeCommandFactory(SettingsContainer settingsContainer)
    {
        _settingsContainer = settingsContainer;
    }

    public ChangeVolumeCommand SetVolume(SoundType soundType, int volume)
    {
        if (!Volumes.ContainsKey(soundType))
        {
            throw new Exception($"SoundType {soundType} needs to be added to {GetType()}.Volumes!");
        }
        Volumes[soundType] = volume;
        _modifiedSoundType = soundType;
        return Create() as ChangeVolumeCommand;
    }

    public override CommandBase Create()
    {
        return new ChangeVolumeCommand(Volumes[_modifiedSoundType], _modifiedSoundType, _settingsContainer);
    }
}