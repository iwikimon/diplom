using System;

namespace IDEService.Core
{
    public class SubsystemFactory
    {
        public static ISubsystem GetSubsystem(SubsystemType type)
        {
            switch (type)
            {
                case SubsystemType.Access:
                    return new AccessSubsystem();
                case SubsystemType.Chat:
                    return new ChatSubsystem();
                case SubsystemType.DataBase:
                    return new DataBaseSubsystem();
                case  SubsystemType.Network:
                    return new NetworkSubsystem();
                case SubsystemType.Project:
                    return new ProjectSubsystem();
                case SubsystemType.Report:
                    return new ReportSubystem();
                case SubsystemType.Vcs:
                    return new VcsSubsystem();
                case SubsystemType.WordProcessor:
                    return new WordProcessorSubsystem();
            }
            throw new SubsystemLoadException("Невозможно загрузить указанную подсистему: "+type);
        }

        public static T GetSubsystemModule<T>(ISubsystem subsystem)
        {
            switch (subsystem.Type())
            {
                case SubsystemType.Access:
                    return ((T)(System.Convert.ChangeType(new AccessModule(subsystem), typeof(T))) );
                case SubsystemType.Chat:
                    return ((T)(System.Convert.ChangeType(new ChatModule(subsystem), typeof(T))));
                //TODO: Добавить вывод модуля для DataBase
                case SubsystemType.Network:
                    return ((T)(System.Convert.ChangeType(new NetworkModule(subsystem), typeof(T))));
                case SubsystemType.Project:
                    return ((T)(System.Convert.ChangeType(new ProjectModule(subsystem), typeof(T))));
                case SubsystemType.Report:
                    return ((T)(System.Convert.ChangeType(new ReportModule(subsystem), typeof(T))));
                case SubsystemType.Vcs:
                    return ((T)(System.Convert.ChangeType(new VcsModule(subsystem), typeof(T))));
                case SubsystemType.WordProcessor:
                    return ((T)(System.Convert.ChangeType(new TextModule(subsystem), typeof(T))));
            }
            throw new SubsystemLoadException("Невозможно загрузить модуль для подсистемы: " + subsystem.Type());
        }
    }
}
