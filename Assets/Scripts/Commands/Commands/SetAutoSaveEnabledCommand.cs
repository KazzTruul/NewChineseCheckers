public class SetAutoSaveEnabledCommand : SynchronousCommand
{
    private SettingsContainer _settingsContainer;
    private bool _enableAutoSave;

    public SetAutoSaveEnabledCommand(SettingsContainer settingsContainer, bool enableAutoSave)
    {
        _settingsContainer = settingsContainer;
        _enableAutoSave = enableAutoSave;
    }

    public override void Execute()
    {
        _settingsContainer.SetAutoSave(_enableAutoSave);
    }
}