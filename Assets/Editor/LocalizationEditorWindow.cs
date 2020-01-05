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

    private const string DeleteTranslationKeyFormat = "Delete {0}";

    private const string GoBackButtonText = "Go Back";

    private static readonly Type TranslationKeysType = typeof(TranslationKeys);

    private static readonly string GeneratedTypeName = TranslationKeysType.Name;

    private enum WindowState
    {
        Default, Add, Delete, Running
    }

    private enum OperationMode
    {
        Add, Delete
    }

    private WindowState _currentWindowState = WindowState.Default;

    private readonly CodeNamespace _generatedNamespace = new CodeNamespace(GeneratedNamespaceName);

    private CodeCompileUnit _targetUnit;

    private CodeTypeDeclaration _targetClass;

    private string _newTranslation = "";

    private int _deletionlGridIndex = -1;

    private int DeletionGridIndex
    {
        set
        {
            if (value == _deletionlGridIndex)
                return;

            _deletionlGridIndex = value;

            if (value > -1)
            {
                var translationKey = FormatStringForTranslationKey(_availableTranslationKeys[_deletionlGridIndex]);

                ConstructGeneratedType(OperationMode.Delete, translationKey, GetOutputFilePath());
            }
        }
    }

    private string[] _availableTranslationKeys;
    #endregion Field

    [MenuItem("Window/Localization/" + EditorWindowName)]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditorWindow>(EditorWindowName);
    }

    private void OnGUI()
    {
        if (EditorApplication.isCompiling)
        {
            return;
        }

        switch (_currentWindowState)
        {
            case WindowState.Default:
                DisplayModeSelection();
                break;

            case WindowState.Add:
                DisplayAddNewTranslationInput();
                DisplayAddNewTranslationButton();
                DisplayGoBackButton();
                break;

            case WindowState.Delete:
                DisplayDeleteTranslationKeys();
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

    private void DisplayModeSelection()
    {
        if (_deletionlGridIndex > -1)
        {
            _deletionlGridIndex = -1;
        }

        GUILayout.BeginVertical();

        if (GUILayout.Button(AddTranslationButtonText))
        {
            _currentWindowState = WindowState.Add;
        }

        if (GUILayout.Button(DeleteTranslationButtonText))
        {
            _currentWindowState = WindowState.Delete;
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
            if (_newTranslation.IsValidTranslationKey())
            {
                ConstructGeneratedType(OperationMode.Add, _newTranslation, GetOutputFilePath());
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

        var buttonTexts = _availableTranslationKeys.Select(s => string.Format(DeleteTranslationKeyFormat, FormatStringForButton(s))).ToArray();

        DeletionGridIndex = GUILayout.SelectionGrid(_deletionlGridIndex, buttonTexts, 5);
    }

    private async void ConstructGeneratedType(OperationMode mode, string targetTranslationKey, string filePath)
    {
        _targetUnit = new CodeCompileUnit();

        _targetClass = new CodeTypeDeclaration(GeneratedTypeName)
        {
            IsClass = true,
            TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
        };

        _generatedNamespace.Types.Add(_targetClass);
        _targetUnit.Namespaces.Add(_generatedNamespace);

        //Add existing translation keys to generated field
        foreach (var existingTranslationKey in TranslationKeysType.GetFields().Where(f => f.FieldType == typeof(string)))
        {
            //Skip a key in order to delete it
            if (mode == OperationMode.Delete && existingTranslationKey.Name == FormatStringForFieldName(targetTranslationKey))
            {
                continue;
            }

            _targetClass.Members.Add(new CodeMemberField
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
            _targetClass.Members.Add(new CodeMemberField
            {
                Type = new CodeTypeReference(typeof(string)),
                Attributes = MemberAttributes.Public | MemberAttributes.Const,
                Name = FormatStringForFieldName(targetTranslationKey),
                InitExpression = new CodePrimitiveExpression(FormatStringForTranslationKey(targetTranslationKey))
            });
        }

        await Task.Run(() => GenerateTypeFile(filePath));

        _currentWindowState = WindowState.Running;
    }

    private async void GenerateTypeFile(string fileName)
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

            using (var streamWriter = new StreamWriter(fileName))
            {
                codeDomProvider.GenerateCodeFromCompileUnit(_targetUnit, streamWriter, codeGeneratorOptions);
            }

            codeDomProvider.Dispose();
        });
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

    private string GetOutputFilePath()
    {
        return Path.Combine(Application.dataPath, "Scripts", "Data", GeneratedNamespaceName, $"{GeneratedTypeName}.cs");
    }
}