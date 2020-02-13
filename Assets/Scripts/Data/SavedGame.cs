using System;
using System.Runtime.Serialization;

[DataContract]
public class SavedGame
{
    [DataMember]
    public int NumberOfPlayers;
    [DataMember]
    public string BoardState;
    [DataMember]
    public Difficulty Difficulty;
    [DataMember]
    public DateTime SavedTime;
}