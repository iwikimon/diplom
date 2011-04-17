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
        Project,
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
