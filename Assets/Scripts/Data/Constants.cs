using UnityEngine;

public static class Constants
{
    //Translations
    #region Translations
    public const string GameNameTranslation = "Game_Name";
    #endregion

    //Paths
    #region Paths
    public const string TranslationJsonName = "Translations.json";
    public static readonly string LocalizationPath = $"{Application.dataPath}/Data/Localization/";
    #endregion

    //Defaults
    #region Defaults
    public const string DefaultLanguage = "en";
    #endregion
}
