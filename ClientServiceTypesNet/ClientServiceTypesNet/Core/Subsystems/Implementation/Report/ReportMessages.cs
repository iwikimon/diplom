using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum ReportMessages
    {
        [EnumMember]
        UserReport,
        [EnumMember]
        ProdjectReport,
        [EnumMember]
        GetUserLog
    }
}
