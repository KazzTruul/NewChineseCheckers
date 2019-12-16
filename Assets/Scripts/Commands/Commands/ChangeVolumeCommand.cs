public class ChangeVolumeCommand : SynchronousCommand
{
    private readonly float _volume;
    private readonly SoundType _soundType;
    private readonly SettingsContainer _settingsContainer;

    public ChangeVolumeCommand(float volume, SoundType soundType, SettingsContainer settingsContainer)
    {
        _volume = volume;
        _soundType = soundType;
        _settingsContainer = settingsContainer;
    }

    public override void Execute()
    {
        _settingsContainer.SetVolume(_soundType, _volume);
    }
}