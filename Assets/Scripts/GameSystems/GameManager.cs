using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System.Text;
using Zenject;

public class GameManager : MonoBehaviour
{
    private ILocalizationManager _localizationManager;

    private Settings _settings;

    public Settings Settings
    {
        get => _settings;
    }
    
    [Inject]
    private void Initialize(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;

        InitializeSettings();
        InitializeLocalization();
    }

    private void InitializeSettings()
    {
        var settingsSerializer = new DataContractJsonSerializer(typeof(Settings),
        new DataContractJsonSerializerSettings
        {
            UseSimpleDictionaryFormat = true
        });


        if (File.Exists(Constants.SettingsPath))
        {
            _settings = settingsSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(File.ReadAllText(Constants.SettingsPath)))) as Settings;
        }
        else
        {
            _settings = new Settings
                (
                Constants.DefaultMasterVolume,
                Constants.DefaultMusicVolume,
                Constants.DefaultSFXVolume,
                _localizationManager.GetPreferredLanguage()
                );

            settingsSerializer.WriteObject(new FileStream(Constants.SettingsPath, FileMode.Create), _settings);
        }
    }

    private void InitializeLocalization()
    {
        _localizationManager.Initialize(_settings.Language);
    }
}
