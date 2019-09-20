public class InitializeLocationCommandFactory : CommandFactory
{
    private readonly SettingsContainer _settingsContainer;
    private readonly ILocalizationManager _localizationManager;

    public InitializeLocationCommandFactory(SettingsContainer settingsContainer, ILocalizationManager localizationManager)
    {
        _settingsContainer = settingsContainer;
        _localizationManager = localizationManager;
    }

    public override CommandBase Create()
    {
        return new InitializeLocalizationCommand(_settingsContainer, _localizationManager);
    }
}