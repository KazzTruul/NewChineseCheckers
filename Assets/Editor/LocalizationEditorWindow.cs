using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Generated;

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
        Default, Add, Delete
    }

    private enum OperationMode
    {
        Add, Delete
    }

    private WindowState _currentWindowState = WindowState.Default;

    private CodeNamespace _generatedNamespace = new CodeNamespace(GeneratedNamespaceName);

    private CodeCompileUnit _targetUnit;

    private CodeTypeDeclaration _targetClass;

    private string _newTranslation = "";
    #endregion Field

    [MenuItem("Window/Localization/" + EditorWindowName)]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditorWindow>(EditorWindowName);
    }

    private void OnGUI()
    {
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
        }
    }

    private void DisplayModeSelection()
    {
        if (GUILayout.Button(AddTranslationButtonText))
        {
            _currentWindowState = WindowState.Add;
        }
        if (GUILayout.Button(DeleteTranslationButtonText))
        {
            _currentWindowState = WindowState.Delete;
        }
    }

    private void DisplayAddNewTranslationInput()
    {
        _newTranslation = EditorGUILayout.TextField(NewTranslationKeyLabel, _newTranslation);
    }

    private void DisplayAddNewTranslationButton()
    {
        if (GUILayout.Button(AddTranslationButtonText))
        {
            ConstructGeneratedType(OperationMode.Add, _newTranslation);
        }
    }

    private void DisplayDeleteTranslationKeys()
    {
        foreach (var stringField in TranslationKeysType.GetFields().Where(f => f.FieldType == typeof(string)))
        {
            var formattedString = FormatStringForButton(stringField.GetValue(stringField) as string);

            if (GUILayout.Button(string.Format(DeleteTranslationKeyFormat, formattedString)))
            {
                ConstructGeneratedType(OperationMode.Delete, stringField.GetValue(stringField) as string);
            }
        }
    }

    private void ConstructGeneratedType(OperationMode mode, string targetTranslationKey)
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
            if (mode == OperationMode.Delete && existingTranslationKey.Name == FormatStringForFieldName(targetTranslationKey))
            {
                Debug.Log("Skipping obsolete key");
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

        GenerateTypeFile(GetOutputFilePath());
    }

    private void GenerateTypeFile(string fileName)
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