using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public class LocalizationManager : ILocalizationManager
{
    private TranslationCatalog _translationCatalog;

    public delegate void LanguageChange(string language);

    public event LanguageChange LanguageChanged;

    private bool IsLanguageSupported(string language)
    {
        return Directory.Exists(
            Path.Combine(Constants.LocalizationPath, language));
    }

    public string GetPreferredLanguage()
    {
        var userLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        return IsLanguageSupported(userLanguage)
            ? userLanguage
            : Constants.DefaultLanguage;
    }

    public void SetPreferredLanguage(string language)
    {
        if (!IsLanguageSupported(language))
            return;
        LoadTranslationCatalog(language);
    }

    public void Initialize(string language)
    {
        LoadTranslationCatalog(IsLanguageSupported(language)
            ? language
            : GetPreferredLanguage());
    }

    private void LoadTranslationCatalog(string language)
    {
        var jsonContent = File.ReadAllText(
            Path.Combine(Constants.LocalizationPath,
            $"{language}/{Constants.TranslationJsonName}"));

        var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

        _translationCatalog = translationSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(jsonContent))) as TranslationCatalog;

        LanguageChanged.Invoke(language);
    }

    public string GetTranslation(string translationId)
    {
        return _translationCatalog.Translations.ContainsKey(translationId) ? _translationCatalog.Translations[translationId] : translationId;
    }
}
