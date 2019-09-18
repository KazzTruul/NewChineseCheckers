using System.Runtime.Serialization;

[DataContract]
public class Settings
{
    [DataMember]
    public int MasterVolume { get; set; }
    [DataMember]
    public int MusicVolume { get; set; }
    [DataMember]
    public int SFXVolume { get; set; }
    [DataMember]
    public string Language { get; set; }
}
