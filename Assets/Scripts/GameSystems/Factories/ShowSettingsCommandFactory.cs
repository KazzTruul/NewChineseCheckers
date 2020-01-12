using Zenject;

public class ShowSettingsCommandFactory
{
    [Inject]
    private SettingsMenuContainer _settingsMenuContainer;

    public ShowSettingsCommand Create(bool showSettings)
    {
        return new ShowSettingsCommand(_settingsMenuContainer, showSettings);
    }
}