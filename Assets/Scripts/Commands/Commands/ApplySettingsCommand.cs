public class ApplySettingsCommand : SynchronousCommand
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