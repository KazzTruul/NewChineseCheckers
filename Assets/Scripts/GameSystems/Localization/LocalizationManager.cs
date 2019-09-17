using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public class LocalizationManager : ILocalizationManager
{
    private TranslationCatalog _translationCatalog;

    public LocalizationManager()
    {
        var userLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        if (!Directory.Exists(Path.Combine(Constants.LocalizationPath, userLanguage)))
        {
            userLanguage = Constants.DefaultLanguage;
        }

        var jsonContent = File.ReadAllText(
            Path.Combine(Constants.LocalizationPath,
            $"{userLanguage}/{Constants.TranslationJsonName}"));

        var translationCatalog = new DataContractJsonSerializer(typeof(TranslationCatalog),
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

        _translationCatalog = translationCatalog.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(jsonContent))) as TranslationCatalog;
    }

    public string GetTranslation(string translationId)
    {
        return _translationCatalog.Translations.ContainsKey(translationId) ? _translationCatalog.Translations[translationId] : translationId;
    }
}
