using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Zenject;

public class LocalizationManager : ILocalizationManager
{
    [Inject]
    private readonly SignalBus _signalBus;
    private TranslationCatalog _translationCatalog;

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
        OnLanguageChanged(language);
    }

    public void Initialize(string language)
    {
        LoadTranslationCatalog(!string.IsNullOrEmpty(language) && IsLanguageSupported(language)
            ? language
            : GetPreferredLanguage());

        OnLanguageChanged(language);
    }

    private void LoadTranslationCatalog(string language)
    {
        var jsonContent = File.ReadAllText(
            Path.Combine(Constants.LocalizationPath,
            $"{language}{Constants.TranslationJsonName}"));

        var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

        _translationCatalog = translationSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(jsonContent))) as TranslationCatalog;
    }

    public string GetTranslation(string translationId)
    {
        return _translationCatalog
            .Translations
            .ContainsKey(translationId) ? 
            _translationCatalog
            .Translations[translationId] : translationId;
    }

    private void OnLanguageChanged(string language)
    {
        _signalBus.Fire(new LanguageChangedSignal() { Language = language });
    }
}
