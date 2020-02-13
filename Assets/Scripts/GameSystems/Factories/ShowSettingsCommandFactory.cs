using Zenject;

public class ShowSettingsCommandFactory
{
    [Inject]
    private readonly SettingsMenuPopupContainer _settingsMenuContainer;

    public ShowSettingsCommand Create(bool showSettings)
    {
        return new ShowSettingsCommand(_settingsMenuContainer, showSettings);
    }
}