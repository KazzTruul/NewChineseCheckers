using System.Runtime.Serialization;

[DataContract]
public sealed class HighScoreEntryData
{
    [DataMember]
    public Difficulty Difficulty { get; set; }
    [DataMember]
    public string PlayerName { get; set; }
    [DataMember]
    public int PlayerScore { get; set; }
    [DataMember]
    public int Position { get; set; }
}