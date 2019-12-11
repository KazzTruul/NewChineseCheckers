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

    private string _currentLanguage = Constants.DefaultLanguage;

    public string CurrentLanguage => _currentLanguage;

    public bool IsLanguageSupported(string language)
    {
        return File.Exists(
            Path.Combine(Constants.LocalizationPath, string.Format(Constants.TranslationJsonName, language)));
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
        language = !string.IsNullOrEmpty(language) && IsLanguageSupported(language)
            ? language
            : GetPreferredLanguage();
        
        LoadTranslationCatalog(language);

        OnLanguageChanged(language);
    }

    private void LoadTranslationCatalog(string language)
    {
        var jsonContent = File.ReadAllText(
            Path.Combine(Constants.LocalizationPath,
            $"{string.Format(Constants.TranslationJsonName, language)}"));

        var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

        using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
        {
            _translationCatalog = translationSerializer.ReadObject(memoryStream) as TranslationCatalog;
        }
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
        _currentLanguage = language;
        _signalBus.Fire(new LanguageChangedSignal() { Language = language });
    }
}
