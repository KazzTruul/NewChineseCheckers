using UnityEngine;

public class SettingsContainer
{
    private Settings _settings;

    public Settings Settings
    {
        get { return _settings; }
        set { if (_settings == null) _settings = value; }
    }

    public void SetLanguage(LanguageChangedSignal signal)
    {
        if (Settings == null)
            return;
        Settings.Language = signal.Language;
    }

    public void SetVolume(SoundType soundType, int volume)
    {
        switch (soundType)
        {
            case SoundType.Master:
                _settings.MasterVolume = volume;
                break;
            case SoundType.Music:
                _settings.MusicVolume = volume;
                break;
            case SoundType.SFX:
                _settings.SFXVolume = volume;
                break;
            default:
                Debug.LogWarning($"Warning! Using unsupported soundtype {soundType}!");
                break;
        }
    }
}

