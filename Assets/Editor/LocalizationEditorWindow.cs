using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Generated;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;

public class LocalizationEditorWindow : EditorWindow
{
    #region Field
    private const string EditorWindowName = "Localization Editor";

    private const string GeneratedNamespaceName = "Generated";

    private const string CodeDomProviderLanguage = "CSharp";

    private const string CodeGeneratorOptionsBracingStyle = "C";

    private const string FieldNameSuffix = "Translation";

    private const string AddTranslationButtonText = "Add New Translation Key";

    private const string DeleteTranslationButtonText = "Delete Existing Translation Key";

    private const string NewTranslationKeyLabel = "New Key Name: ";

    private const string NewSupportedLanguageLabel = "New Language: ";

    private const string AddLanguageButtonText = "Add Supported Language";

    private const string DeleteLanguageButtonText = "Delete Supported Language";

    private const string GoBackButtonText = "Go Back";

    private const string SupportedLanguagesFieldArrayName = "Languages";

    private static readonly Type TranslationKeysType = typeof(TranslationKeys);

    private static readonly Type SupportedLanguagesType = typeof(SupportedLanguages);

    private static readonly string TranslationKeysGeneratedTypeName = TranslationKeysType.Name;

    private static readonly string SupportedLanguagesGeneratedTypeName = SupportedLanguagesType.Name;

    private enum WindowState
    {
        Default, AddTranslation, DeleteTranslation, AddSupportedLanguage, DeleteSupportedLanguage, Running
    }

    private enum OperationMode
    {
        Add, Delete
    }

    private WindowState _currentWindowState = WindowState.Default;

    private readonly CodeNamespace _generatedNamespace = new CodeNamespace(GeneratedNamespaceName);

    private CodeCompileUnit _targetUnit;

    private CodeTypeDeclaration _translationKeysClass;

    private CodeTypeDeclaration _supportedLanguagesClass;

    private string _newTranslation = "";

    private int _translationKeyDeletionlGridIndex = -1;

    private string _newLanguage = "";

    private int _supportedLanguageDeletionGridIndex = -1;

    private int TranslationKeyDeletionGridIndex
    {
        set
        {
            if (value == _translationKeyDeletionlGridIndex)
                return;

            _translationKeyDeletionlGridIndex = value;

            if (value > -1)
            {
                var translationKey = FormatStringForTranslationKey(_availableTranslationKeys[_translationKeyDeletionlGridIndex]);

                ConstructTranslationKeysType(OperationMode.Delete, translationKey, GetTranslationKeyOutputFilePath(), GetTranslationCatalogPaths());
            }
        }
    }

    private int SupportedLanguageDeletionGridIndex
    {
        set
        {
            if (value == _supportedLanguageDeletionGridIndex)
                return;

            _supportedLanguageDeletionGridIndex = value;

            if (value > -1)
            {
                var newLanguage = _availableSupportedLanguages[_supportedLanguageDeletionGridIndex];
                ConstructSupportedLanguagesTypeFile(newLanguage, GetSupportedLanguagesOutputFilePath(), GetTranslationCatalogPath(newLanguage), OperationMode.Delete);
            }
        }
    }

    private string[] _availableTranslationKeys;

    private string[] _availableSupportedLanguages;
    #endregion Field

    [MenuItem("Window/Localization/" + EditorWindowName)]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditorWindow>(EditorWindowName);
    }

    private void OnGUI()
    {
        if (EditorApplication.isCompiling || Application.isPlaying)
        {
            return;
        }

        switch (_currentWindowState)
        {
            case WindowState.Default:
                DisplayModeSelection();
                break;

            case WindowState.AddTranslation:
                DisplayAddNewTranslationInput();
                DisplayAddNewTranslationButton();
                DisplayGoBackButton();
                break;

            case WindowState.DeleteTranslation:
                DisplayDeleteTranslationKeys();
                DisplayGoBackButton();
                break;

            case WindowState.AddSupportedLanguage:
                DisplayAddSupportedLanguageInput();
                DisplayAddSupportedLanguageButton();
                DisplayGoBackButton();
                break;

            case WindowState.DeleteSupportedLanguage:
                DisplayDeleteSupportedLanguages();
                DisplayGoBackButton();
                break;

            case WindowState.Running:
                Event.current.Use();
                EditorGUILayout.TextArea("Operation in progress...");
                AssetDatabase.Refresh();
                EditorApplication.update += WaitForRecompilation;
                break;
        }
    }

    private void WaitForRecompilation()
    {
        if (!EditorApplication.isCompiling)
        {
            EditorApplication.update -= WaitForRecompilation;
            _currentWindowState = WindowState.Default;
        }
    }

    #region LayoutElementsDisplay
    private void DisplayModeSelection()
    {
        if (_translationKeyDeletionlGridIndex > -1)
        {
            _translationKeyDeletionlGridIndex = -1;
        }

        if (_supportedLanguageDeletionGridIndex > -1)
        {
            _supportedLanguageDeletionGridIndex = -1;
        }

        GUILayout.BeginVertical();

        if (GUILayout.Button(AddTranslationButtonText))
        {
            _currentWindowState = WindowState.AddTranslation;
        }

        if (GUILayout.Button(DeleteTranslationButtonText))
        {
            _currentWindowState = WindowState.DeleteTranslation;
        }

        if (GUILayout.Button(AddLanguageButtonText))
        {
            _currentWindowState = WindowState.AddSupportedLanguage;
        }

        if (GUILayout.Button(DeleteLanguageButtonText))
        {
            _currentWindowState = WindowState.DeleteSupportedLanguage;
        }

        GUILayout.EndVertical();
    }

    private void DisplayAddNewTranslationInput()
    {
        _newTranslation = EditorGUILayout.TextField(NewTranslationKeyLabel, _newTranslation);
    }

    private void DisplayAddNewTranslationButton()
    {
        if (GUILayout.Button(AddTranslationButtonText))
        {
            _newTranslation = _newTranslation.CapitalizeInitialLetters();

            if (_newTranslation.IsValidTranslationKey())
            {
                if (TranslationKeysType.GetField(FormatStringForFieldName(_newTranslation)) == null)
                {
                    ConstructTranslationKeysType(OperationMode.Add, _newTranslation, GetTranslationKeyOutputFilePath(), GetTranslationCatalogPaths());
                }
                else
                {
                    Debug.LogError("Key Already Exists!");
                }
            }
            else
            {
                Debug.LogError("Invalid Translation Key");
            }
        }
    }

    private void DisplayDeleteTranslationKeys()
    {
        _availableTranslationKeys = TranslationKeysType.GetFields()
            .Where(f => f.FieldType == typeof(string))
            .Select(s => s.GetValue(s) as string)
            .ToArray();

        var buttonTexts = _availableTranslationKeys.Select(s => FormatStringForButton(s)).ToArray();

        TranslationKeyDeletionGridIndex = GUILayout.SelectionGrid(_translationKeyDeletionlGridIndex, buttonTexts, 5);
    }

    private void DisplayAddSupportedLanguageInput()
    {
        _newLanguage = EditorGUILayout.TextField(NewSupportedLanguageLabel, _newLanguage);
    }

    private void DisplayAddSupportedLanguageButton()
    {
        if (GUILayout.Button(AddLanguageButtonText))
        {
            _newLanguage = _newLanguage.ToLower();

            if (_newLanguage.IsValidLanguage())
            {
                if (!_availableSupportedLanguages.Contains(_newLanguage))
                {
                    ConstructSupportedLanguagesTypeFile(_newLanguage, GetSupportedLanguagesOutputFilePath(), GetTranslationCatalogPath(_newLanguage), OperationMode.Add);
                }
                else
                {
                    Debug.LogError("Language already supported!");
                }
            }
            else
            {
                Debug.LogError("Invalid Language format! Please use two or three letter ISO code!");
            }
        }
    }

    private void DisplayDeleteSupportedLanguages()
    {
        _availableSupportedLanguages = SupportedLanguages.Languages.Where(l => l != Constants.DefaultLanguage).ToArray();

        SupportedLanguageDeletionGridIndex = GUILayout.SelectionGrid(_supportedLanguageDeletionGridIndex, _availableSupportedLanguages, 5);
    }

    private void DisplayGoBackButton()
    {
        if (GUILayout.Button(GoBackButtonText))
        {
            _newTranslation = "";
            _currentWindowState = WindowState.Default;
            GUI.FocusControl(AddTranslationButtonText);
        }
    }
    #endregion LayoutElementDisplays

    #region TranslationKeysGeneration
    private async void ConstructTranslationKeysType(OperationMode mode, string targetTranslationKey, string keysFilePath, string[] catalogFilePaths)
    {
        await Task.Run(() =>
        {
            _targetUnit = new CodeCompileUnit();

            _translationKeysClass = new CodeTypeDeclaration(TranslationKeysGeneratedTypeName)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            _generatedNamespace.Types.Add(_translationKeysClass);
            _targetUnit.Namespaces.Add(_generatedNamespace);

            //Add existing translation keys to generated field
            foreach (var existingTranslationKey in TranslationKeysType.GetFields().Where(f => f.FieldType == typeof(string)))
            {
                //Skip a key in order to delete it
                if (mode == OperationMode.Delete && existingTranslationKey.Name == FormatStringForFieldName(targetTranslationKey))
                {
                    continue;
                }

                _translationKeysClass.Members.Add(new CodeMemberField
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
                _translationKeysClass.Members.Add(new CodeMemberField
                {
                    Type = new CodeTypeReference(typeof(string)),
                    Attributes = MemberAttributes.Public | MemberAttributes.Const,
                    Name = FormatStringForFieldName(targetTranslationKey),
                    InitExpression = new CodePrimitiveExpression(FormatStringForTranslationKey(targetTranslationKey))
                });
            }

            var taskList = new List<Task>
            {
                Task.Run(() => GenerateTypeFile(keysFilePath))
            };

            foreach (var catalogFilePath in catalogFilePaths)
            {
                taskList.Add(Task.Run(() => UpdateTranslationCatalogKeys(catalogFilePath, targetTranslationKey, mode)));
            }

            Task.WaitAll(taskList.ToArray());
        });

        _currentWindowState = WindowState.Running;
    }

    private async void GenerateTypeFile(string filePath)
    {
        await Task.Run(() =>
        {
            if (!CodeDomProvider.IsDefinedLanguage(CodeDomProviderLanguage))
            {
                throw new Exception($"{CodeDomProviderLanguage} is not a valid language!");
            }

            var codeDomProvider = CodeDomProvider.CreateProvider(CodeDomProviderLanguage);

            var codeGeneratorOptions = new CodeGeneratorOptions
            {
                BracingStyle = CodeGeneratorOptionsBracingStyle,
                BlankLinesBetweenMembers = false
            };

            using (var streamWriter = new StreamWriter(filePath))
            {
                codeDomProvider.GenerateCodeFromCompileUnit(_targetUnit, streamWriter, codeGeneratorOptions);
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

            if (mode == OperationMode.Add)
            {
                translationCatalog.AddTranslation(translationKey);
            }
            else if (mode == OperationMode.Delete)
            {
                translationCatalog.DeleteTranslation(translationKey);
            }
            else
            {
                throw new Exception("Invalid OperationMode!");
            }

            using (var stream = File.Create(translationCatalogFilePath))
            {
                translationSerializer.WriteObject(stream, translationCatalog);
            }
        });
    }
    #endregion TranslationKeysGeneration

    #region SupportedLanguagesGeneration
    private async void ConstructSupportedLanguagesTypeFile(string targetLanguage, string outputTypeFilePath, string outputCatalogPath, OperationMode mode)
    {
        await Task.Run(() =>
        {
            _targetUnit = new CodeCompileUnit();

            _supportedLanguagesClass = new CodeTypeDeclaration(SupportedLanguagesGeneratedTypeName)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            _generatedNamespace.Types.Add(_supportedLanguagesClass);
            _targetUnit.Namespaces.Add(_generatedNamespace);

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

            GenerateTypeFile(outputTypeFilePath);

            GenerateTranslationCatalog(outputCatalogPath, mode);
        });

        _currentWindowState = WindowState.Running;
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

            foreach (var translationKey in TranslationKeysType.GetFields()
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
    #endregion SupportedLanguagesGeneration

    #region Formatting
    private string FormatStringForButton(string unformattedString)
    {
        return unformattedString.Replace('_', ' ');
    }

    private string FormatStringForTranslationKey(string unformattedString)
    {
        return unformattedString.Replace(' ', '_');
    }

    private string FormatStringForFieldName(string unformattedString)
    {
        var formattedString = "";

        foreach (var c in unformattedString)
        {
            if (char.IsLetterOrDigit(c))
            {
                formattedString += c;
            }
        }

        formattedString += FieldNameSuffix;

        return formattedString;
    }
    #endregion Formatting

    #region Paths
    //Required to get file path as these can't be retrieved outside of main thread
    private string GetTranslationKeyOutputFilePath()
    {
        return Path.Combine(Application.dataPath, "Scripts", "Data", GeneratedNamespaceName, $"{TranslationKeysGeneratedTypeName}.cs");
    }

    //Required to get file path as these can't be retrieved outside of main thread
    private string GetSupportedLanguagesOutputFilePath()
    {
        return Path.Combine(Application.dataPath, "Scripts", "Data", GeneratedNamespaceName, $"{SupportedLanguagesGeneratedTypeName}.cs");
    }

    //Required to get file path as these can't be retrieved outside of main thread
    private string GetTranslationCatalogPath(string language)
    {
        return Path.Combine(Constants.LocalizationPath, string.Format(Constants.TranslationJsonName, language));
    }

    //Required to get file paths as these can't be retrieved outside of main thread
    private string[] GetTranslationCatalogPaths()
    {
        var catalogFilePaths = new string[SupportedLanguages.Languages.Length];

        for (var i = 0; i < catalogFilePaths.Length; i++)
        {
            catalogFilePaths[i] = Path.Combine(Constants.LocalizationPath, string.Format(Constants.TranslationJsonName, SupportedLanguages.Languages[i]));
        }

        return catalogFilePaths;
    }
    #endregion Paths
}