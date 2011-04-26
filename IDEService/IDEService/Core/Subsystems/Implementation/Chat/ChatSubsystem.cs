using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEService.Core
{
    class ChatSubsystem : Subsystem
    {
        private IChatModule _module;
        public ChatSubsystem() : base(SubsystemType.Chat)
        {
        }

        public override bool Start()
        {
            _module = new ChatModule(this);
            return true;
        }

        public override bool Stop()
        {
            _module.Dispose();
            return true;
        }

        public override bool Reload()
        {
            throw new NotImplementedException();
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            /*var msgType = ChatMessages.Undefined;
            try
            {
                msgType = (ChatMessages)Convert.ChangeType(message.Type, typeof(ChatMessages));
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                                 String.Format("Неправильный тип сообщения: {0} для подсистемы {1}",
                                                message.Type, this.Type()));
            }
            switch (msgType)
            {
                    
            }
            */
            return message;
        }


    }
}
