using UnityEngine;

public static class Constants
{
    //Translations
    #region Translations
    public const string GameNameTranslation = "Game_Name";
    public const string OptionsTranslation = "Game_Options";
    public const string SaveGameTranslation = "Game_Save";
    public const string LoadGameTranslation = "Game_Load";
    public const string SinglePlayerTranslation = "GameMode_Single";
    public const string MultiPlayerTranslation = "GameMode_Multi";
    #endregion

    //Paths
    #region Paths
    public const string TranslationJsonName = "Translations.json";
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
