public class InitializeSettingsCommandFactory
{
    private readonly SettingsContainer _settingsContainer;
    private readonly ILocalizationManager _localizationManager;

    public InitializeSettingsCommandFactory(SettingsContainer settingsContainer, ILocalizationManager localizationManager)
    {
        _settingsContainer = settingsContainer;
        _localizationManager = localizationManager;
    }

    public InitializeSettingsCommand Create()
    {
        return new InitializeSettingsCommand(_settingsContainer, _localizationManager);
    }
}