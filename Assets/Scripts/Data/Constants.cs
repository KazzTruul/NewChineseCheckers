using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    #region Input
    public const KeyCode PauseKey = KeyCode.Escape;
    public const int MinUsernameLength = 6;
    public const int MaxUsernameLength = 16;
    public const int MinPasswordLength = 6;
    public const int MaxPasswordLength = 100;
    #endregion Input

    #region Paths
    public const string TranslationJsonName = "{0}Translations.json";
    public static readonly string LocalizationPath = $"{Application.dataPath}/Data/Localization/";
    public static readonly string SettingsPath = $"{Application.persistentDataPath}/Settings.json";
    #endregion Paths

    #region Defaults
    public const float DefaultMasterVolume = 0f;
    public const float DefaultMusicVolume = 0f;
    public const float DefaultSFXVolume = 0f;
    public const string DefaultLanguage = "en";
    public const bool AutoSaveDefault = true;
    public const bool AutoLoginDefault = true;
    #endregion Defaults

    #region SceneIndices
    public const int MasterSceneIndex = 0;
    public const int LoginSceneIndex = 1;
    public const int MainMenuSceneIndex = 2;
    public const int SinglePlayerSceneIndex = 3;
    public const int MultiPlayerSceneIndex = 4;
    #endregion SceneIndices

    #region Conversions
    public static readonly Dictionary<BoardColor, Color> ColorConversions = new Dictionary<BoardColor, Color>
    {
        { BoardColor.White, Color.white },
        { BoardColor.Yellow, Color.yellow },
        { BoardColor.Blue, Color.blue },
        { BoardColor.Green, Color.green },
        { BoardColor.Red, Color.red },
        { BoardColor.Magenta, Color.magenta },
        { BoardColor.Black, Color.black },
    };

    public static readonly Dictionary<string, string> IsoToLocalizedLanguages = new Dictionary<string, string>
    {
        { "en", "English" },
        { "sv", "Svenska" }
    };
    #endregion Conversions
}