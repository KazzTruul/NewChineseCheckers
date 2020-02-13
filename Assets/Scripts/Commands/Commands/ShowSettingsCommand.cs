public class ShowSettingsCommand : SynchronousCommand
{
    private readonly SettingsMenuPopupContainer _settingsMenuContainer;
    private readonly bool _showSettings;

    public ShowSettingsCommand(SettingsMenuPopupContainer settingsMenuContainer, bool showSettings)
    {
        _settingsMenuContainer = settingsMenuContainer;
        _showSettings = showSettings;
    }

    public override void Execute()
    {
        _settingsMenuContainer.ShowSettings(_showSettings);
    }
}