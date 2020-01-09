using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Generated;

namespace LocalizationEditor
{
    public class LocalizationEditorWindow : EditorWindow
    {
        #region Field
        private WindowState _currentWindowState = WindowState.Default;

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
                    var translationKey = EditorWindowOperationsBase.FormatStringForTranslationKey(_availableTranslationKeys[_translationKeyDeletionlGridIndex]);

                    new TranslationKeysOperations(this).ConstructTranslationKeysType(OperationMode.Delete, translationKey, GetTranslationKeyOutputFilePath(), GetTranslationCatalogPaths());
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
                    var obsoleteLanguage = _availableSupportedLanguages[_supportedLanguageDeletionGridIndex];
                    new SupportedLanguagesOperations(this).ConstructSupportedLanguagesTypeFile(obsoleteLanguage, GetSupportedLanguagesOutputFilePath(), GetTranslationCatalogPath(obsoleteLanguage), OperationMode.Delete);
                }
            }
        }

        private string[] _availableTranslationKeys;

        private string[] _availableSupportedLanguages;
        #endregion Field

        [MenuItem("Window/Localization/" + LocalizationEditorData.EditorWindowName)]
        public static void ShowWindow()
        {
            GetWindow<LocalizationEditorWindow>(LocalizationEditorData.EditorWindowName);
        }

        private void OnGUI()
        {
            if (EditorApplication.isCompiling)
            {
                GUILayout.BeginArea(new Rect(0, 0, position.width, position.height));
                EditorGUILayout.LabelField("Operation in progress...", new GUIStyle() { fontSize = 30/*, alignment = TextAnchor.MiddleCenter*/ });
                GUILayout.EndArea();
                return;
            }
            if (Application.isPlaying)
            {
                GUILayout.BeginArea(new Rect(0, 0, position.width, position.height));
                EditorGUILayout.LabelField("Application running.", new GUIStyle() { fontSize = 30/*, alignment = TextAnchor.MiddleCenter*/ });
                GUILayout.EndArea();
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

            if (GUILayout.Button(LocalizationEditorData.AddTranslationButtonText))
            {
                _currentWindowState = WindowState.AddTranslation;
            }

            if (GUILayout.Button(LocalizationEditorData.DeleteTranslationButtonText))
            {
                _currentWindowState = WindowState.DeleteTranslation;
            }

            if (GUILayout.Button(LocalizationEditorData.AddLanguageButtonText))
            {
                _currentWindowState = WindowState.AddSupportedLanguage;
            }

            if (GUILayout.Button(LocalizationEditorData.DeleteLanguageButtonText))
            {
                _currentWindowState = WindowState.DeleteSupportedLanguage;
            }

            GUILayout.EndVertical();
        }

        private void DisplayAddNewTranslationInput()
        {
            _newTranslation = EditorGUILayout.TextField(LocalizationEditorData.NewTranslationKeyLabel, _newTranslation);
        }

        private void DisplayAddNewTranslationButton()
        {
            if (GUILayout.Button(LocalizationEditorData.AddTranslationButtonText))
            {
                _newTranslation = _newTranslation.CapitalizeInitialLetters();

                if (_newTranslation.IsValidTranslationKey())
                {
                    if (LocalizationEditorData.TranslationKeysType.GetField(EditorWindowOperationsBase.FormatStringForFieldName(_newTranslation)) == null)
                    {
                        new TranslationKeysOperations(this).ConstructTranslationKeysType(OperationMode.Add, _newTranslation, GetTranslationKeyOutputFilePath(), GetTranslationCatalogPaths());
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
            _availableTranslationKeys = LocalizationEditorData.TranslationKeysType.GetFields()
                .Where(f => f.FieldType == typeof(string))
                .Select(s => s.GetValue(s) as string)
                .ToArray();

            var buttonTexts = _availableTranslationKeys.Select(s => EditorWindowOperationsBase.FormatStringForButton(s)).ToArray();

            TranslationKeyDeletionGridIndex = GUILayout.SelectionGrid(_translationKeyDeletionlGridIndex, buttonTexts, 5);
        }

        private void DisplayAddSupportedLanguageInput()
        {
            _newLanguage = EditorGUILayout.TextField(LocalizationEditorData.NewSupportedLanguageLabel, _newLanguage);
        }

        private void DisplayAddSupportedLanguageButton()
        {
            if (GUILayout.Button(LocalizationEditorData.AddLanguageButtonText))
            {
                _newLanguage = _newLanguage.ToLower();

                if (_newLanguage.IsValidLanguage())
                {
                    if (!_availableSupportedLanguages.Contains(_newLanguage))
                    {
                        new SupportedLanguagesOperations(this).ConstructSupportedLanguagesTypeFile(_newLanguage, GetSupportedLanguagesOutputFilePath(), GetTranslationCatalogPath(_newLanguage), OperationMode.Add);
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
            if (GUILayout.Button(LocalizationEditorData.GoBackButtonText))
            {
                _newTranslation = "";
                _currentWindowState = WindowState.Default;
                GUI.FocusControl(LocalizationEditorData.AddTranslationButtonText);
            }
        }
        #endregion LayoutElementDisplays


        public void SetRunning()
        {
            _currentWindowState = WindowState.Running;
        }
        
        #region Paths
        //Required to get file path as these can't be retrieved outside of main thread
        private string GetTranslationKeyOutputFilePath()
        {
            return Path.Combine(Application.dataPath, "Scripts", "Data", LocalizationEditorData.GeneratedNamespaceName, $"{LocalizationEditorData.TranslationKeysGeneratedTypeName}.cs");
        }

        //Required to get file path as these can't be retrieved outside of main thread
        private string GetSupportedLanguagesOutputFilePath()
        {
            return Path.Combine(Application.dataPath, "Scripts", "Data", LocalizationEditorData.GeneratedNamespaceName, $"{LocalizationEditorData.SupportedLanguagesGeneratedTypeName}.cs");
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
}