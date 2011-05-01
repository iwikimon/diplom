using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    [DataContract]
    public enum Action
    {
        [EnumMember]
        Added,
        [EnumMember]
        Removed
    }
    
    [DataContract]
    public class StructureChange
    {
        [DataMember]
        public Action Action { get; set; }

        [DataMember]
        public DBModelDtoBase DtoEntity { get; set; }

        public StructureChange(){ }

        public StructureChange(Action action, DBModelDtoBase entity)
        {
            Action = action;
            DtoEntity = entity;
        }
    }
}
