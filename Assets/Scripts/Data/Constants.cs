using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    #region Localization
    public static readonly string[] SupportedLanuages =
    {
        "en",
        "sv"
    };

    public static readonly Dictionary<TranslationIdentifier, string> Translations = new Dictionary<TranslationIdentifier, string>()
    {
        { TranslationIdentifier.GameName,               "Game_Name" },
        { TranslationIdentifier.Options,                "Game_Options" },
        { TranslationIdentifier.SaveGame,               "Game_Save" },
        { TranslationIdentifier.LoadGame,               "Game_Load" },
        { TranslationIdentifier.QuitGame,               "Game_Quit" },
        { TranslationIdentifier.SinglePlayer,           "Game_Start_Single" },
        { TranslationIdentifier.MultiPlayer,            "Game_Start_Multi" },
        { TranslationIdentifier.DifficultyEasy,         "Game_Difficulty_Easy" },
        { TranslationIdentifier.DifficultyMedium,       "Game_Difficulty_Medium" },
        { TranslationIdentifier.DifficultyHard,         "Game_Difficulty_Hard" },
        { TranslationIdentifier.OptionsTitle,           "Options_Title" },
        { TranslationIdentifier.MasterVolume,           "Volume_Master" },
        { TranslationIdentifier.MusicVolume,            "Volume_Music" },
        { TranslationIdentifier.SFXVolume,              "Volume_SFX" },
        { TranslationIdentifier.ChangeLanguage,         "Language_Change" },
        { TranslationIdentifier.GoBack,                 "Menu_Return" },
        { TranslationIdentifier.ResumeGame,             "Game_Resume" },
        { TranslationIdentifier.RestartGame,            "Game_Restart" },
        { TranslationIdentifier.MainMenu,               "Main_Menu" },
        { TranslationIdentifier.PauseMenuTitle,         "Pause_Menu_Title" },
        { TranslationIdentifier.Language,               "Options_Language" },
        { TranslationIdentifier.AutoSave,               "Options_AutoSave" },
        { TranslationIdentifier.SaveAndLeave,           "Options_SaveAndLeave" },
        { TranslationIdentifier.LoginMenuTitle,         "Login_Menu_Title" },
        { TranslationIdentifier.PlayerUsername,      "Player_Display_Name" },
        { TranslationIdentifier.PlayerPassword,         "Player_Password" },
        { TranslationIdentifier.Login,                  "Player_Login" },
        { TranslationIdentifier.CreateAccount,          "Player_Create_Account" }
    };
    #endregion Localization

    #region Input
    public const KeyCode PauseKey = KeyCode.Escape;
    public const int MinUsernameLength = 6;
    public const int MaxUsernameLength = 16;
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