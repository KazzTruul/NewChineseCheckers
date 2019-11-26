public class ChangeLanguageCommandFactory
{
    private ILocalizationManager _localizationManager;
    private string _language = Constants.DefaultLanguage;

    public string Language
    {
        get { return _language; }
        set { if (_localizationManager.IsLanguageSupported(value)) { _language = value; } }
    }

    public ChangeLanguageCommand Create()
    {
        return new ChangeLanguageCommand(_localizationManager, _language);
    }
}