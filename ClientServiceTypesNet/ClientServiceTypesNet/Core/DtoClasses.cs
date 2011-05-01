using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    [System.Runtime.Serialization.DataContract(Name = "DBModelDtoBase")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class DBModelDtoBase
    {
    }

    [System.Runtime.Serialization.DataContract(Name = "Chat")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class ChatDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public System.DateTime Date { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Message { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int ProjectId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "File")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class FileDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Path { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int FolderId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "Folder")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class FolderDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Path { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int ProjectId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "Project")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class ProjectDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Sourcedir { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int OwnerId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "Userlog")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class UserlogDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public System.DateTime Date { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Message { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "User")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class UserDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public string Login { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Password { get; set; }
        [System.Runtime.Serialization.DataMember]
        public System.DateTime Registred { get; set; }
        [System.Runtime.Serialization.DataMember]
        public System.DateTime LastAccess { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Name { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Sname { get; set; }
        [System.Runtime.Serialization.DataMember]
        public string Email { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "Access")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class AccessDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public string Rule { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int FileId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int FolderId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int UserId { get; set; }
    }

    [System.Runtime.Serialization.DataContract(Name = "ProdjectMember")]
    [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
    public partial class ProdjectMemberDto : DBModelDtoBase
    {
        [System.Runtime.Serialization.DataMember]
        public int ProjectId { get; set; }
        [System.Runtime.Serialization.DataMember]
        public int UserId { get; set; }
    }

}
