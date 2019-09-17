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

    public Settings(
        int masterVolume,
        int musicVolume,
        int sfxVolume,
        string language)
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SFXVolume = sfxVolume;
        Language = language;
    }
}
