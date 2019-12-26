using System.IO;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class SettingsContainer
{
    private SettingsData _settings;

    public SettingsData Settings
    {
        get { return _settings; }
    }

    private SettingsData _unsavedSettings = new SettingsData();

    public void InitializeSettings(SettingsData settings)
    {
        _settings = settings;
        _unsavedSettings = settings;
    }

    public void SetLanguage(LanguageChangedSignal signal)
    {
        _unsavedSettings.Language = signal.Language;
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

    public void SetAutoLogin(bool autoLogin)
    {
        _unsavedSettings.AutoLogin = autoLogin;
    }

    public void SetCurrentUser(string username, string password)
    {
        _unsavedSettings.Username = username;
        _unsavedSettings.Password = password;

        SaveChanges();
    }

    public void SaveChanges()
    {
        _settings.OverwriteSettings(_unsavedSettings);

        var settingsSerializer = new DataContractJsonSerializer(typeof(SettingsData),
        new DataContractJsonSerializerSettings
        {
            UseSimpleDictionaryFormat = true
        });

        settingsSerializer.WriteObject(new FileStream(Constants.SettingsPath, FileMode.Create), _settings);
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