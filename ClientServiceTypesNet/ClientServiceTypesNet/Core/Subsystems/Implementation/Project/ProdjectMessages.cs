using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public enum ProjectMessages
    {
        [EnumMember]
        CreateProject,
        [EnumMember]
        DeleteProject,
        [EnumMember]
        SelectProject,
        [EnumMember]
        GetProjectList,
        [EnumMember]
        GetStructure,
        [EnumMember]
        RunProject,
        [EnumMember]
        AddFolder,
        [EnumMember]
        AddFile,
        [EnumMember]
        AddMember,
        [EnumMember]
        RemoveFolder,
        [EnumMember]
        RemoveFile,
        [EnumMember]
        RemoveMember,
        [EnumMember]
        Update,
        [EnumMember]
        Undefined
    }
}
