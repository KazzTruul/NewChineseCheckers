using Zenject;

public class ChangeLanguageCommandFactory
{
    [Inject]
    private readonly ILocalizationManager _localizationManager;

    public ChangeLanguageCommand Create(string language)
    {
        return new ChangeLanguageCommand(_localizationManager, language);
    }
}
