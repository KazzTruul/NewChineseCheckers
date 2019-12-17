using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public sealed class LeaderboardsData
{
    [DataMember]
    public List<HighScoreEntryData> Easy { get; set; }
    [DataMember]
    public List<HighScoreEntryData> Medium { get; set; }
    [DataMember]
    public List<HighScoreEntryData> Hard { get; set; }
}