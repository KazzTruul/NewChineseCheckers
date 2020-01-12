public class ShowSettingsCommand : SynchronousCommand
{
    private readonly SettingsMenuContainer _settingsMenuContainer;
    private readonly bool _showSettings;

    public ShowSettingsCommand(SettingsMenuContainer settingsMenuContainer, bool showSettings)
    {
        _settingsMenuContainer = settingsMenuContainer;
        _showSettings = showSettings;
    }

    public override void Execute()
    {
        _settingsMenuContainer.ShowSettings(_showSettings);
    }
}