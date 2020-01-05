using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class TranslationCatalog
{
    [DataMember]
    public Dictionary<string, string> Translations = new Dictionary<string, string>();
}
