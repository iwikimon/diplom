using System;
using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract]
    public class ChatMessage
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Message { get; set; }

        public ChatMessage()
        {
        }

        public ChatMessage(String user, DateTime date, string message)
        {
            User = user;
            Date = date;
            Message = message;
        }
    }
}
