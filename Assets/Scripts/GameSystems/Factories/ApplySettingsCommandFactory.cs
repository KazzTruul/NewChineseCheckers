using Zenject;

public class ApplySettingsCommandFactory
{
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    public SynchronousCommand Create()
    {
        return new ApplySettingsCommand(_settingsContainer);
    }
}