using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum SubsystemState
    {
        [EnumMember]
        Working,
        [EnumMember]
        Stopped,
        [EnumMember]
        Reloading,
        [EnumMember]
        Loading,
        [EnumMember]
        Loaded,
        [EnumMember]
        Unknown
    }
}
