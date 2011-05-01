using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    [DataContract]
    public class ProjectStructure
    {
        [DataMember]
        public List<FolderDto> Folders { get; set; }

        [DataMember]
        public List<FileDto> Files { get; set; } 

        public ProjectStructure()
        {
            Folders = new List<FolderDto>();
            Files = new List<FileDto>();
        }
    }
}
