public class ApplySettingsCommand : CommandBase
{
    private readonly SettingsContainer _settingsContainer;

    public ApplySettingsCommand(SettingsContainer settingsContainer)
    {
        _settingsContainer = settingsContainer;
    }

    public override void Execute()
    {

    }
}