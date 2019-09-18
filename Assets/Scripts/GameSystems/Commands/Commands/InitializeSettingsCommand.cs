using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using Zenject;

public class InitializeSettingsCommand : CommandBase
{
    private Settings _settings;
    private ILocalizationManager _localizationManager;

    [Inject]
    public void Initialize(Settings settings, ILocalizationManager localizationManager)
    {
        _settings = settings;
        _localizationManager = localizationManager;
    }

    public override void Execute()
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
            _settings = new Settings{
                MasterVolume = Constants.DefaultMasterVolume,
                MusicVolume = Constants.DefaultMusicVolume,
                SFXVolume = Constants.DefaultSFXVolume,
                Language = _localizationManager.GetPreferredLanguage()
            };

            settingsSerializer.WriteObject(new FileStream(Constants.SettingsPath, FileMode.Create), _settings);
        }
    }
}
