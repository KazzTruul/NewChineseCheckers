using System;
using System.CodeDom;
using Generated;

namespace LocalizationEditor
{
    internal static class LocalizationEditorData
    {
        public const string EditorWindowName = "Localization Editor";

        public const string GeneratedNamespaceName = "Generated";

        public const string CodeDomProviderLanguage = "CSharp";

        public const string CodeGeneratorOptionsBracingStyle = "C";

        public const string FieldNameSuffix = "Translation";

        public const string AddTranslationButtonText = "Add New Translation Key";

        public const string DeleteTranslationButtonText = "Delete Existing Translation Key";

        public const string NewTranslationKeyLabel = "New Key Name: ";

        public const string NewSupportedLanguageLabel = "New Language: ";

        public const string AddLanguageButtonText = "Add Supported Language";

        public const string DeleteLanguageButtonText = "Delete Supported Language";

        public const string GoBackButtonText = "Go Back";

        public static readonly Type TranslationKeysType = typeof(TranslationKeys);

        public static readonly Type SupportedLanguagesType = typeof(SupportedLanguages);

        public static readonly string TranslationKeysGeneratedTypeName = TranslationKeysType.Name;

        public static readonly string SupportedLanguagesGeneratedTypeName = SupportedLanguagesType.Name;

        public static readonly CodeNamespace GeneratedNamespace = new CodeNamespace(GeneratedNamespaceName);
    }
}