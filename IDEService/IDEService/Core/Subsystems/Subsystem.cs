using System;

namespace IDEService.Core
{
    abstract public class Subsystem : ISubsystem
    {
        public readonly SubsystemInfo Info;
        private bool _dispose = false;
        
        protected Subsystem(SubsystemType type)
        {
            Info = new SubsystemInfo {State = SubsystemState.Loading, Type = type};
            Info.State = SubsystemState.Loaded;
            Info.StartTime = DateTime.Now;
            Logger.Log.Write(LogLevels.Debug, "Создание подсистемы типа " + type.ToString());
            Info.State = SubsystemState.Working;
        }

        public void Dispose()
        {
            if (!_dispose)
            {
                _dispose = true;
                if (!this.Stop())
                {
                    throw new SubsystemStopException("Ошибка остановки подсистемы "+this.Info.Type);
                }
                Kernel.GetKernel.UnRegisterSubsystem(this);
                Logger.Log.Write(LogLevels.Debug,String.Format("Подсистема {0} отключена", Info.Type));
            }
        }

        public  abstract ServiceMessage SendMessage(ServiceMessage message);

        public SubsystemType Type()
        {
            return Info.Type;
        }

        public SubsystemState GetState()
        {
            return Info.State;
        }

        public SubsystemInfo GetInfo()
        {
            return Info;
        }

        public abstract bool Start();
        public abstract bool Stop();
        public abstract bool Reload();
    }
}
