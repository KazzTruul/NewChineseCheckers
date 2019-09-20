public class InitializeSettingsCommandFactory : CommandFactory
{
    private readonly SettingsContainer _settingsContainer;
    private readonly ILocalizationManager _localizationManager;

    public InitializeSettingsCommandFactory(SettingsContainer settingsContainer, ILocalizationManager localizationManager)
    {
        _settingsContainer = settingsContainer;
        _localizationManager = localizationManager;
    }

    public override CommandBase Create()
    {
        return new InitializeSettingsCommand(_settingsContainer, _localizationManager);
    }
}