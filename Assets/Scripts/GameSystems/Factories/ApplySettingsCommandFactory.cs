public class ApplySettingsCommandFactory : CommandFactory
{
    private readonly SettingsContainer _settingsContainer;

    public ApplySettingsCommandFactory(SettingsContainer settingsContainer)
    {
        _settingsContainer = settingsContainer;
    }

    public override CommandBase Create()
    {
        return new ApplySettingsCommand(_settingsContainer);
    }
}