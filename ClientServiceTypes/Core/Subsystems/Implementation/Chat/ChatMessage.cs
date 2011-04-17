using System;

namespace IDEService.Core
{
    class ChatMessage
    {
        public string User { get; set; }

        public DateTime Date { get; set; }

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
