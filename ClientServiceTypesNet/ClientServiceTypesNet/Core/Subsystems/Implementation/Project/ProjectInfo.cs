using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    /// <summary>
    /// Представляет информацию о проекте
    /// </summary>
    [DataContract]
    public class ProjectInfo
    {
        /// <summary>
        /// Владелец проекта
        /// </summary>
        [DataMember]
        public string Owner { get; set; }

        /// <summary>
        /// Список участников
        /// </summary>
        [DataMember]
        public List<string> Members { get; set; }

        /// <summary>
        /// Имя проекта
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        public ProjectInfo(string name, string owner, List<string> members)
        {
            Name = name;
            Owner = owner;
            Members = members;
        }
    }
}
