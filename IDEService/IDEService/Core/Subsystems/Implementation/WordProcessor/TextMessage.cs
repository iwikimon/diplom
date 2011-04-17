using System.Runtime.Serialization;

namespace IDEService.Core
{
    [DataContract] 
    public class TextMessage
    {
        [DataMember] 
        public TextPosition From { get; set; }

        [DataMember] 
        public TextPosition To { get; set; }

        [DataMember] 
        public string Username { get; set; }

        [DataMember] 
        public string OldValue { get; set; }

        [DataMember] 
        public string NewValue { get; set; }

        public TextMessage(){ }

        public TextMessage(string username, TextPosition from, TextPosition to, string oldValue, string newValue )
        {
            Username = username;
            From = from;
            To = to;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
