public class ChangeLanguageCommand : SynchronousCommand
{
    private readonly ILocalizationManager _localizationManager;
    private readonly string _language;

    public ChangeLanguageCommand(ILocalizationManager localizationManager, string language)
    {
        _localizationManager = localizationManager;
        _language = language;
    }

    public override void Execute()
    {
        _localizationManager.SetPreferredLanguage(_language);
    }
}