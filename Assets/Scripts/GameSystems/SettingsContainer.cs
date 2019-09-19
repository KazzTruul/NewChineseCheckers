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
}

