public class ChangeLanguageCommandFactory : CommandFactory
{
    private ILocalizationManager _localizationManager;
    private string _language = Constants.DefaultLanguage;

    public string Language
    {
        get { return _language; }
        set { if (_localizationManager.IsLanguageSupported(value)) { _language = value; } }
    }

    public override CommandBase Create()
    {
        return new ChangeLanguageCommand(_localizationManager, _language);
    }
}