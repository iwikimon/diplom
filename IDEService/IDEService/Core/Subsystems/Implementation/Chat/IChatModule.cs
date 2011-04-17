using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    interface IChatModule : IDisposable
    {
        List<ChatMessage> GetHistory();

        void SendMessage(ChatMessage msg);

        List<ChatMessage> GetMessages();
    }

}
