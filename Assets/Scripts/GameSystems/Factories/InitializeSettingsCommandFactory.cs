using Zenject;

public class InitializeSettingsCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;
    [Inject]
    private readonly ILocalizationManager _localizationManager;
    
    public InitializeSettingsCommand Create()
    {
        return new InitializeSettingsCommand(_settingsContainer, _localizationManager);
    }
}