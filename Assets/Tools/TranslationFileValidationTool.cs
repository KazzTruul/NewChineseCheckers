using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System;

internal sealed class TranslationFileValidationTool
{
    private static readonly string LocalizationsDirectory = $"{Application.dataPath}/Data/Localization";
    private const string TranslationsFileName = "{0}Translations.json";
    private const string PlaceholderTranslation = "[PLACEHOLDER TRANSLATION]";
    private const string SearchStartedMessage = "Validating translation files...";
    private const string SearchFinishedMessage = "Translation files validated.";
    private const string LocalizationDirectoryNotFoundMessage = "Localization directory not found at expected path {0}.\nAborting translation files validation.";
    private const string IdentifierMissingFromConstantsMessage = "TranslationKey {0} defined as a TranslationIdentifier value, but not added to Constants.Translation.\nEither add a KeyPairValue for {0} to Constants.Translations or remove it from the TranslationIdentifier Enum.";
    private const string MissingLanguageFileMessage = "Supported language \"{0}\" does not have a translation file. Please create one or remove {0} from supported languages.";
    private const string TranslationKeyMissingFromConstantsMessage = "Translation key \"{0}\" in {1} not found among TranslationConstants. Add key to TranslationConstants or remove from translation file.";
    private const string AddingPlaceholderTranslationMessage = "Translation key \"{0}\" not found in {1}. Adding placeholder translation.";

    [MenuItem("Tools/Validate Translation Files")]
    public static async Task ValidateTranslationFiles()
    {
        if (!Directory.Exists(LocalizationsDirectory))
        {
            Debug.Log(string.Format(LocalizationDirectoryNotFoundMessage, LocalizationsDirectory));
            return;
        }
        Debug.Log(SearchStartedMessage);

        var fileUpdateTasks =
            from language
            in Constants.SupportedLanuages
            select Task.Run(() => ValidateTranslationFile(language));

        await Task.WhenAll(fileUpdateTasks);

        Debug.Log(SearchFinishedMessage);
    }

    private static async Task ValidateTranslationFile(string language)
    {
        var fileName = string.Format(TranslationsFileName, language);
        var directoryFileName = Path.Combine(LocalizationsDirectory, fileName);

        if (!File.Exists(directoryFileName))
        {
            Debug.Log(string.Format(MissingLanguageFileMessage, language));
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

            var translationKeyFields = typeof(TranslationKeys).GetFields()
                .Where(f => f.FieldType == typeof(string))
                .Select(f => f.GetValue(f) as string)
                .ToArray();

            var missingConstantTranslationsInCatalog =
            from translation
            in catalog.Translations.Keys
            where Array.IndexOf(translationKeyFields, translation) < 0
            select translation;

            foreach (var missingTranslation in missingConstantTranslationsInCatalog)
            {
                Debug.Log(string.Format(TranslationKeyMissingFromConstantsMessage, missingTranslation, fileName));
            }

            var missingCatalogTranslations =
            from translation
            in translationKeyFields
            where !catalog.Translations.ContainsKey(translation)
            select translation;

            foreach (var missingTranslation in missingCatalogTranslations)
            {
                Debug.Log(string.Format(AddingPlaceholderTranslationMessage, missingTranslation, fileName));
                catalog.Translations.Add(missingTranslation, PlaceholderTranslation);
            }

            using (var fileStream = File.Create(directoryFileName))
            {
                translationSerializer.WriteObject(fileStream, catalog);
            }
        });
    }
}