public class ApplySettingsCommandFactory
{
    private readonly SettingsContainer _settingsContainer;

    public ApplySettingsCommandFactory(SettingsContainer settingsContainer)
    {
        _settingsContainer = settingsContainer;
    }

    public SynchronousCommand Create()
    {
        return new ApplySettingsCommand(_settingsContainer);
    }
}