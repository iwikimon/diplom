using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum SubsystemType
    {
        [EnumMember]
        DataBase,
        [EnumMember]
        Access,
        [EnumMember]
        Network,
        [EnumMember]
        Chat,
        [EnumMember]
        Prodject,
        [EnumMember]
        Vcs,
        [EnumMember]
        Report,
        [EnumMember]
        WordProcessor,
        [EnumMember]
        Undefined
    }
}
