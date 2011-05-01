using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IDEService.Core
{
    [DataContract]
    public class Cache
    {

        [DataMember]
        public DateTime LoginTime { get; set; }
        [DataMember]
        public DateTime DisconnectTime { get; set; }
        [DataMember]
        public string IP { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя в текущей сессии
        /// </summary>
        [DataMember]
        public int UID { get; set; }
        [DataMember]
        public List<ChatMessage> ChatMessages { get; set; }
        [DataMember]
        public List<TextMessage> TextMessages { get; set; }

        [DataMember]
        public List<UserlogDto> LogMessages { get; set; }

        [DataMember]
        public ProjectDto CurrentProject { get; set; }

        [DataMember]
        public FileDto CurrentFile { get; set; }

        [DataMember]
        public List<StructureChange> StructureChanges { get; set; }

        [DataMember]
        public List<ProjectDto> NewProjects { get; set; }

        public Cache()
        {
            ChatMessages = new List<ChatMessage>();
            TextMessages = new List<TextMessage>();
            LogMessages = new List<UserlogDto>();
            StructureChanges = new List<StructureChange>();
            NewProjects = new List<ProjectDto>();
            LoginTime = DateTime.Now;

        }

        /// <summary>
        /// Очищает очереди входящих сообщений.
        /// </summary>
        public void Clear()
        {
            ChatMessages.Clear();
            TextMessages.Clear();
            LogMessages.Clear();
            StructureChanges.Clear();
            NewProjects.Clear();
        }

    }
}
