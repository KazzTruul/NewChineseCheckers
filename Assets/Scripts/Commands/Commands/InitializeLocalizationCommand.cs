public class InitializeLocalizationCommand : SynchronousCommand
{
    private readonly SettingsContainer _settingsContainer;
    private readonly ILocalizationManager _localizationManager;

    public InitializeLocalizationCommand(SettingsContainer settingsManager, ILocalizationManager localizationManager)
    {
        _settingsContainer = settingsManager;
        _localizationManager = localizationManager;
    }

    public override void Execute()
    {
        _localizationManager.Initialize(_settingsContainer.Settings.Language);
    }
}