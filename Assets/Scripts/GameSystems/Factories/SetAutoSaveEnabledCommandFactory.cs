using Zenject;

public class SetAutoSaveEnabledCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    public SetAutoSaveEnabledCommand Create(bool enableAutoSave)
    {
        return new SetAutoSaveEnabledCommand(_settingsContainer, enableAutoSave);
    }
}