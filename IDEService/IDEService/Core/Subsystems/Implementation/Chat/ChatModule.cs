using System;
using System.Collections.Generic;

namespace IDEService.Core
{
    class ChatModule :IChatModule
    {
        private readonly ISubsystem _chatSubsystem;
        
        public ChatModule(ISubsystem chatSubsystem)
        {
            if (chatSubsystem.Type() != SubsystemType.Chat)
                throw new ModuleLoadException("Неправильный тип подсистемы");
            _chatSubsystem = chatSubsystem;
        }

        public List<ChatMessage> GetHistory()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ChatMessage msg)
        {
            
        }

        public List<ChatMessage> GetMessages()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
