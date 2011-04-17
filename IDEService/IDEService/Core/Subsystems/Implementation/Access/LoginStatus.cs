using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum LoginStatus
    {
        [EnumMember]
        Free,
        [EnumMember]
        Busy
    }
}
