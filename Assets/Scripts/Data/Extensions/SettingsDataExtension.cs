public static class SettingsDataExtension
{
    public static void OverwriteSettings(this SettingsData settingsData, SettingsData newSettings)
    {
        settingsData.MasterVolume = newSettings.MasterVolume;
        settingsData.MusicVolume = newSettings.MusicVolume;
        settingsData.SFXVolume = newSettings.SFXVolume;
        settingsData.Language = newSettings.Language;
        settingsData.AutoSave = newSettings.AutoSave;
    }

    public static void ResetToDefaultSettings(this SettingsData settingsData, ILocalizationManager localizationManager)
    {
        settingsData.MasterVolume = Constants.DefaultMasterVolume;
        settingsData.MusicVolume = Constants.DefaultMusicVolume;
        settingsData.SFXVolume = Constants.DefaultSFXVolume;
        settingsData.Language = localizationManager.GetPreferredLanguage();
        settingsData.AutoSave = Constants.AutoSaveDefault;
    }
}