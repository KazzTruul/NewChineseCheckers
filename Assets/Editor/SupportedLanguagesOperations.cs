using System;
using System.CodeDom;
using System.IO;
using System.Linq;
using System.Reflection;
using Generated;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.CodeDom.Compiler;

namespace LocalizationEditor
{    internal sealed class SupportedLanguagesOperations : EditorWindowOperationsBase
    {
        private const string SupportedLanguagesFieldArrayName = "Languages";

        private static readonly Type SupportedLanguagesType = typeof(SupportedLanguages);
        private static readonly string SupportedLanguagesGeneratedTypeName = SupportedLanguagesType.Name;

        private readonly LocalizationEditorWindow _localizationEditorWindow;

        private CodeTypeDeclaration _supportedLanguagesClass;

        public SupportedLanguagesOperations(LocalizationEditorWindow localizationEditorWindow)
        {
            _localizationEditorWindow = localizationEditorWindow;
        }

        public async void ConstructSupportedLanguagesTypeFile(string targetLanguage, string outputTypeFilePath, string outputCatalogPath, OperationMode mode)
        {
            await Task.Run(() =>
            {
                var targetUnit = new CodeCompileUnit();

                _supportedLanguagesClass = new CodeTypeDeclaration(SupportedLanguagesGeneratedTypeName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
                };

                LocalizationEditorData.GeneratedNamespace.Types.Add(_supportedLanguagesClass);
                targetUnit.Namespaces.Add(LocalizationEditorData.GeneratedNamespace);

                var fieldArrayInfo = SupportedLanguagesType.GetField(SupportedLanguagesFieldArrayName);

                var fieldArray = fieldArrayInfo.GetValue(fieldArrayInfo) as string[];

                //Add new language to the type being constructed
                if (mode == OperationMode.Add)
                {
                    var fieldList = fieldArray.ToList();
                    fieldList.Add(targetLanguage);
                    fieldArray = fieldList.ToArray();
                }

                _supportedLanguagesClass.Members.Add(new CodeMemberField
                {
                    Type = new CodeTypeReference(typeof(string[])),
                    Attributes = MemberAttributes.Public | MemberAttributes.Static,
                    Name = SupportedLanguagesFieldArrayName,
                    InitExpression = new CodeArrayCreateExpression(new CodeTypeReference(typeof(string)),
                    fieldArray
                        .Where(l => l != targetLanguage || mode != OperationMode.Delete)
                        .Select(l => new CodePrimitiveExpression(l))
                        .ToArray())
                });

                GenerateTypeFile(outputTypeFilePath, targetUnit);

                GenerateTranslationCatalog(outputCatalogPath, mode);
            });

            _localizationEditorWindow.SetRunning();
        }

        private async void GenerateTypeFile(string filePath, CodeCompileUnit targetUnit)
        {
            await Task.Run(() =>
            {
                if (!CodeDomProvider.IsDefinedLanguage(LocalizationEditorData.CodeDomProviderLanguage))
                {
                    throw new Exception($"{LocalizationEditorData.CodeDomProviderLanguage} is not a valid language!");
                }

                var codeDomProvider = CodeDomProvider.CreateProvider(LocalizationEditorData.CodeDomProviderLanguage);

                var codeGeneratorOptions = new CodeGeneratorOptions
                {
                    BracingStyle = LocalizationEditorData.CodeGeneratorOptionsBracingStyle,
                    BlankLinesBetweenMembers = false
                };

                using (var streamWriter = new StreamWriter(filePath))
                {
                    codeDomProvider.GenerateCodeFromCompileUnit(targetUnit, streamWriter, codeGeneratorOptions);
                }

                codeDomProvider.Dispose();
            });
        }

        private async void GenerateTranslationCatalog(string catalogPath, OperationMode mode)
        {
            await Task.Run(() =>
            {
                if (mode == OperationMode.Delete)
                {
                    if (File.Exists(catalogPath))
                    {
                        File.Delete(catalogPath);
                    }
                    return;
                }

                var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
                new DataContractJsonSerializerSettings
                {
                    UseSimpleDictionaryFormat = true
                });

                var translationCatalog = new TranslationCatalog();

                foreach (var translationKey in typeof(TranslationKeys).GetFields()
                    .Where(f => f.FieldType == typeof(string))
                    .Select(f => f.GetValue(f) as string))
                {
                    translationCatalog.AddTranslation(translationKey);
                }

                using (var stream = File.Create(catalogPath))
                {
                    translationSerializer.WriteObject(stream, translationCatalog);
                }
            });
        }
    }
}