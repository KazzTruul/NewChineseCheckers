using Zenject;

public class InitializeLocationCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;
    [Inject]
    private readonly ILocalizationManager _localizationManager;
    
    public InitializeLocalizationCommand Create()
    {
        return new InitializeLocalizationCommand(_settingsContainer, _localizationManager);
    }
}