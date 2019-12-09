using System.Runtime.Serialization;

[DataContract]
public class SettingsData
{
    [DataMember]
    public float MasterVolume { get; set; }
    [DataMember]
    public float MusicVolume { get; set; }
    [DataMember]
    public float SFXVolume { get; set; }
    [DataMember]
    public string Language { get; set; }
    [DataMember]
    public bool AutoSave { get; set; }
}
