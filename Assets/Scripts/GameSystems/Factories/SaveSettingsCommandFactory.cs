using Zenject;

public class SaveSettingsCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    public SaveSettingsCommand Create()
    {
        return new SaveSettingsCommand(_settingsContainer.Settings);
    }
}