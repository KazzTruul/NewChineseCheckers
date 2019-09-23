public interface ILocalizationManager
{
    string GetTranslation(string translationId);
    void Initialize(string language);
    string GetPreferredLanguage();
    void SetPreferredLanguage(string language);
    bool IsLanguageSupported(string language);
}
