using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

internal sealed class TranslationFileValidationTool
{
    private static readonly string LocalizationsDirectory = $"{Application.dataPath}/Data/Localization";
    private const string TranslationsFileName = "{0}Translations.json";
    private const string PlaceholderTranslation = "[PLACEHOLDER TRANSLATION]";

    [MenuItem("Tools/Validate Translation Files")]
    public static async Task ValidateTranslationFiles()
    {
        if (!Directory.Exists(LocalizationsDirectory))
        {
            Debug.Log($"Localization directory not found at expected path {LocalizationsDirectory}.\nAborting translation files validation.");
            return;
        }
        Debug.Log("Validating translation files...");

        var fileUpdateTasks =
            from language
            in Constants.SupportedLanuages
            select Task.Run(() => ValidateTranslationFile(language));

        await Task.WhenAll(fileUpdateTasks);

        Debug.Log("Translation files validated.");
    }

    private static async Task ValidateTranslationFile(string language)
    {
        var fileName = string.Format(TranslationsFileName, language);
        var directoryFileName = Path.Combine(LocalizationsDirectory, fileName);

        if (!File.Exists(directoryFileName))
        {
            Debug.Log($"Supported language \"{language}\" does not have a translation file. Please create one or remove {language} from supported languages.");
            return;
        }
        
        await Task.Run(() =>
        {
            var jsonContent = File.ReadAllText(
                Path.Combine(Constants.LocalizationPath,
                $"{string.Format(Constants.TranslationJsonName, language)}"));

            var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
                new DataContractJsonSerializerSettings
                {
                    UseSimpleDictionaryFormat = true
                });

            TranslationCatalog catalog;
            using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
            {
                catalog = translationSerializer.ReadObject(memoryStream) as TranslationCatalog;
            }

            var missingConstantTranslations =
            from translation
            in catalog.Translations.Keys
            where !Constants.Translations.ContainsValue(translation)
            select translation;

            foreach (var missingTranslation in missingConstantTranslations)
            {
                Debug.Log($"Translation key \"{missingTranslation}\" in {fileName} not found among TranslationConstants. Add key to TranslationConstants or remove from translation file.");
            }

            var missingCatalogTranslations =
            from translation
            in Constants.Translations.Values
            where !catalog.Translations.ContainsKey(translation)
            select translation;

            foreach (var missingTranslation in missingCatalogTranslations)
            {
                Debug.Log($"Translation key \"{missingTranslation}\" not found in {fileName}. Adding placeholder translation.");
                catalog.Translations.Add(missingTranslation, PlaceholderTranslation);
            }

            using (var fileStream = File.Create(directoryFileName))
            {
                translationSerializer.WriteObject(fileStream, catalog);
            }
        });
    }
}