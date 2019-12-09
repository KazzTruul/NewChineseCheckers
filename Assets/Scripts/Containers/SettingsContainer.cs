using UnityEngine;

public class SettingsContainer
{
    private SettingsData _settings;

    public SettingsData Settings
    {
        get { return _settings; }
        set { if (_settings == null) _settings = value; }
    }

    private SettingsData _unsavedSettings = new SettingsData();

    public void SetLanguage(LanguageChangedSignal signal)
    {
        if (Settings == null)
            return;

        Settings.Language = signal.Language;
    }

    public void SetVolume(SoundType soundType, float volume)
    {
        switch (soundType)
        {
            case SoundType.Master:
                _unsavedSettings.MasterVolume = volume;
                break;
            case SoundType.Music:
                _unsavedSettings.MusicVolume = volume;
                break;
            case SoundType.SFX:
                _unsavedSettings.SFXVolume = volume;
                break;
            default:
                Debug.LogWarning($"Warning! Using unsupported soundtype {soundType}!");
                break;
        }
    }

    public void SetAutoSave(bool autoSave)
    {
        _unsavedSettings.AutoSave = autoSave;
    }

    public void SaveChanges()
    {
        _settings.OverwriteSettings(_unsavedSettings);
    }

    public void DiscardChanges()
    {
        _unsavedSettings.OverwriteSettings(_settings);
    }

    public void ResetToDefault(ILocalizationManager localizationManager)
    {
        _unsavedSettings.ResetToDefaultSettings(localizationManager);
    }
}