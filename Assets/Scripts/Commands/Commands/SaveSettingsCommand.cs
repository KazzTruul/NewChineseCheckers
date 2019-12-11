using System.IO;
using System.Runtime.Serialization.Json;

public class SaveSettingsCommand : SynchronousCommand
{
    private readonly SettingsData _settingsData;

    public SaveSettingsCommand(SettingsData settingsData)
    {
        _settingsData = settingsData;
    }

    public override void Execute()
    {
        var settingsSerializer = new DataContractJsonSerializer(typeof(SettingsData),
        new DataContractJsonSerializerSettings
        {
            UseSimpleDictionaryFormat = true
        });
        
        settingsSerializer.WriteObject(new FileStream(Constants.SettingsPath, FileMode.Create), _settingsData);
    }
}