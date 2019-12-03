using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public class InitializeSettingsCommand : SynchronousCommand
{
    private readonly SettingsContainer _settingsContainer;
    private readonly ILocalizationManager _localizationManager;

    public InitializeSettingsCommand(SettingsContainer settingsManager, ILocalizationManager localizationManager)
    {
        _settingsContainer = settingsManager;
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
            _settingsContainer.Settings = settingsSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(File.ReadAllText(Constants.SettingsPath)))) as Settings;
        }
        else
        {
            _settingsContainer.Settings = new Settings{
                MasterVolume = Constants.DefaultMasterVolume,
                MusicVolume = Constants.DefaultMusicVolume,
                SFXVolume = Constants.DefaultSFXVolume,
                Language = _localizationManager.GetPreferredLanguage()
            };

            settingsSerializer.WriteObject(new FileStream(Constants.SettingsPath, FileMode.Create), _settingsContainer);
        }
    }
}
