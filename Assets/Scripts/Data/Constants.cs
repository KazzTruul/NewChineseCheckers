using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Localization
    #region Localization
    public static readonly string[] SupportedLanuages =
    {
        "en",
        "sv"
    };

    public static readonly Dictionary<TranslationIdentifier, string> Translations = new Dictionary<TranslationIdentifier, string>()
    {
        { TranslationIdentifier.GameNameTranslation,     "Game_Name" },
        { TranslationIdentifier.OptionsTranslation,      "Game_Options" },
        { TranslationIdentifier.SaveGameTranslation,     "Game_Save" },
        { TranslationIdentifier.LoadGameTranslation,     "Game_Load" },
        { TranslationIdentifier.QuitGameTranslation,     "Game_Quit" },
        { TranslationIdentifier.SinglePlayerTranslation, "Game_Start_Single" },
        { TranslationIdentifier.MultiPlayerTranslation,  "Game_Start_Multi" },
        { TranslationIdentifier.DifficultyEasy,          "Game_Difficulty_Easy" },
        { TranslationIdentifier.DifficultyMedium,        "Game_Difficulty_Medium" },
        { TranslationIdentifier.DifficultyHard,          "Game_Difficulty_Hard" },
        { TranslationIdentifier.OptionsTitle,            "Options_Title" },
        { TranslationIdentifier.MasterVolume,            "Volume_Master" },
        { TranslationIdentifier.MusicVolume,             "Volume_Music" },
        { TranslationIdentifier.SFXVolume,               "Volume_SFX" },
        { TranslationIdentifier.ChangeLanguage,          "Language_Change" },
        { TranslationIdentifier.GoBack,                  "Menu_Return" }
    };
    #endregion

    //Paths
    #region Paths
    public const string TranslationJsonName = "{0}Translations.json";
    public static readonly string LocalizationPath = $"{Application.dataPath}/Data/Localization/";
    public static readonly string SettingsPath = $"{Application.persistentDataPath}/Settings.json";
    #endregion

    //Defaults
    #region Defaults
    public const int DefaultMasterVolume = 0;
    public const int DefaultMusicVolume = 0;
    public const int DefaultSFXVolume = 0;
    public const string DefaultLanguage = "en";
    #endregion
}
