using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum AccessMessages
    {
        [EnumMember]
        Login,
        [EnumMember]
        Logout,
        [EnumMember]
        CheckLogin,
        [EnumMember]
        Register,
        [EnumMember]
        GetInfo,
        [EnumMember]
        Update,
        [EnumMember]
        Undefined
    }
}
