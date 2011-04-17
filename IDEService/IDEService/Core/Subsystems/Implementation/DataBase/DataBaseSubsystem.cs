using System;
using Mindscape.LightSpeed;

namespace IDEService.Core
{
    class DataBaseSubsystem :Subsystem
    {
        private DBModelUnitOfWork context = new LightSpeedContext<DBModelUnitOfWork>("Development").CreateUnitOfWork();
        public DataBaseSubsystem() : base(SubsystemType.DataBase)
        {

        }

        public override bool Start()
        {
            return true;
        }

        public override bool Stop()
        {
            return true;
        }

        public override bool Reload()
        {
            throw new NotImplementedException();
        }

        public override ServiceMessage SendMessage(ServiceMessage message)
        {
            var msgType = DbSubsystemMessages.Undefined;
            try
            {
                msgType = (DbSubsystemMessages)Convert.ChangeType(message.Type, typeof(DbSubsystemMessages));
            }
            catch (Exception)
            {
                Logger.Log.Write(LogLevels.Emerg,
                                 String.Format("Неправильный тип сообщения: {0} для подсистемы {1}",
                                                message.Type, this.Type()));
            }
            switch (msgType)
            {
                case DbSubsystemMessages.GetContext:
                    return new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.DataBase, message.From, DbSubsystemMessages.GetContext, new object[] { context });
                case DbSubsystemMessages.SaveContext:
                    {
                        context.SaveChanges();
                        return new ServiceMessage(KernelTypes.ServiceKernel, SubsystemType.DataBase, message.From, DbSubsystemMessages.SaveContext, new object[] { });
                    }
                default:
                    throw new SubsystemWorkingException("Неопознанный тип сообщения.");
            }
        }
    }
}
