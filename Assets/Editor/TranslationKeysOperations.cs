using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using Generated;

namespace LocalizationEditor
{
    internal sealed class TranslationKeysOperations : EditorWindowOperationsBase
    {
        private static readonly Type TranslationKeysType = typeof(TranslationKeys);

        private static readonly string TranslationKeysGeneratedTypeName = TranslationKeysType.Name;

        private readonly LocalizationEditorWindow _localizationEditorWindow;

        public TranslationKeysOperations(LocalizationEditorWindow localizationEditorWindow)
        {
            _localizationEditorWindow = localizationEditorWindow;
        }

        public async void ConstructTranslationKeysType(OperationMode mode, string targetTranslationKey, string keysFilePath, string[] catalogFilePaths)
        {
            await Task.Run(() =>
            {
                var targetUnit = new CodeCompileUnit();

                var translationKeysClass = new CodeTypeDeclaration(TranslationKeysGeneratedTypeName)
                {
                    IsClass = true,
                    TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
                };

                LocalizationEditorData.GeneratedNamespace.Types.Add(translationKeysClass);
                targetUnit.Namespaces.Add(LocalizationEditorData.GeneratedNamespace);

                //Add existing translation keys to generated field
                foreach (var existingTranslationKey in TranslationKeysType.GetFields().Where(f => f.FieldType == typeof(string)))
                {
                    //Skip a key in order to delete it
                    if (mode == OperationMode.Delete && existingTranslationKey.Name == FormatStringForFieldName(targetTranslationKey))
                    {
                        continue;
                    }

                    translationKeysClass.Members.Add(new CodeMemberField
                    {
                        Type = new CodeTypeReference(typeof(string)),
                        Attributes = MemberAttributes.Public | MemberAttributes.Const,
                        Name = existingTranslationKey.Name,
                        InitExpression = new CodePrimitiveExpression(existingTranslationKey.GetValue(existingTranslationKey))
                    });
                }

                if (mode == OperationMode.Add)
                {
                    //Add new translation key to generated field
                    translationKeysClass.Members.Add(new CodeMemberField
                    {
                        Type = new CodeTypeReference(typeof(string)),
                        Attributes = MemberAttributes.Public | MemberAttributes.Const,
                        Name = FormatStringForFieldName(targetTranslationKey),
                        InitExpression = new CodePrimitiveExpression(FormatStringForTranslationKey(targetTranslationKey))
                    });
                }

                var taskList = new List<Task>
                {
                    Task.Run(() => GenerateTypeFile(keysFilePath, targetUnit))
                };

                foreach (var catalogFilePath in catalogFilePaths)
                {
                    taskList.Add(Task.Run(() => UpdateTranslationCatalogKeys(catalogFilePath, targetTranslationKey, mode)));
                }

                Task.WaitAll(taskList.ToArray());
            });

            _localizationEditorWindow.SetRunning();
        }

        public async void GenerateTypeFile(string filePath, CodeCompileUnit targetUnit)
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

        private async void UpdateTranslationCatalogKeys(string translationCatalogFilePath, string translationKey, OperationMode mode)
        {
            await Task.Run(() =>
            {
                var jsonContent = File.ReadAllText(translationCatalogFilePath);

                var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
                new DataContractJsonSerializerSettings
                {
                    UseSimpleDictionaryFormat = true
                });

                TranslationCatalog translationCatalog;

                using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonContent)))
                {
                    translationCatalog = translationSerializer.ReadObject(memoryStream) as TranslationCatalog;
                }

                switch (mode)
                {
                    case OperationMode.Add:
                        translationCatalog.AddTranslation(translationKey);
                        break;

                    case OperationMode.Delete:
                        translationCatalog.DeleteTranslation(translationKey);
                        break;

                    default:
                        throw new Exception("Invalid OperationMode!");
                }

                using (var stream = File.Create(translationCatalogFilePath))
                {
                    translationSerializer.WriteObject(stream, translationCatalog);
                }
            });
        }
    }
}