using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LocalizationEditorWindow : EditorWindow
{
    private const string EditorWindowName = "Localization Editor";

    private const string AddTranslationButtonText = "Add New Translation Key";

    private const string DeleteTranslationButtonText = "Delete Existing Translation Key";

    private const string UpdateTranslationButtonText = "Update Existing Translation Key";

    private const string NewTranslationKeyLabel = "New Key Name: ";

    private const string DeleteTranslationKeyFormat = "Delete {0}";

    private const string UpdateTranslationKeyFormat = "Update {0}";

    private const string GoBackButtonText = "Go Back";

    private static readonly Type TranslationKeysType = typeof(TranslationKeys);

    private enum WindowState
    {
        Default, Add, Delete, Update
    }

    private WindowState _windowState = WindowState.Default;

    private string _newTranslation = "";

    private Dictionary<WindowState, string> ExistingTranslationFormats = new Dictionary<WindowState, string>
    {
        { WindowState.Delete, DeleteTranslationKeyFormat },
        { WindowState.Update, UpdateTranslationKeyFormat }
    };

    [MenuItem("Window/Localization/" + EditorWindowName)]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditorWindow>($"Localization Editor{EditorWindowName}");
    }

    private void OnGUI()
    {
        switch (_windowState)
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

            case WindowState.Update:

                DisplayGoBackButton();
                break;
        }
    }

    private void DisplayModeSelection()
    {
        if (GUILayout.Button(AddTranslationButtonText))
        {
            _windowState = WindowState.Add;
        }
        if (GUILayout.Button(DeleteTranslationButtonText))
        {
            _windowState = WindowState.Delete;
        }
        if (GUILayout.Button(UpdateTranslationButtonText))
        {
            _windowState = WindowState.Update;
        }
    }

    private void DisplayAddNewTranslationInput()
    {
        _newTranslation = EditorGUILayout.TextField(NewTranslationKeyLabel, _newTranslation);
    }

    private void DisplayAddNewTranslationButton()
    {

    }

    private void AddNewTranslation(string newTranslation)
    {

    }

    private void DisplayDeleteTranslationKeys()
    {
        DisplayExistingTranslationKeys(WindowState.Delete);
    }

    private void DisplayUpdateTranslationKeys()
    {
        DisplayExistingTranslationKeys(WindowState.Update);
    }

    private void DisplayExistingTranslationKeys(WindowState windowState)
    {
        foreach (var stringField in TranslationKeysType.GetFields().Where(f => f.FieldType == typeof(string)))
        {
            var formattedString = FormatStringForButton(stringField.GetValue(stringField) as string);

            if (GUILayout.Button(string.Format(ExistingTranslationFormats[windowState], formattedString)))
            {
                //switch (windowState)
                //{

                //}
            }
        }
    }

    private void DisplayGoBackButton()
    {
        if (GUILayout.Button(GoBackButtonText))
        {
            _newTranslation = "";
            _windowState = WindowState.Default;
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
}