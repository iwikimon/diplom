using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IDEService.Core
{
    public class LoadException : Exception
    {
        public LoadException(string msg) : base(msg) { }
    } 

    /// <summary>
    /// Отвечает за загрузку подсистем и компонентьов сервиса.
    /// </summary>
    public class BootLoader
    {
        /// <summary>
        /// Загрузка подсистем
        /// </summary>
        /// <typeparam name="T">Загружаемый тип</typeparam>
        /// <param name="file">Директория библиотек</param>
        /// <returns></returns>
        public static T LoadType<T>(string file)
        {
            /*IEnumerable<T> a = Directory.GetFiles(folder, "*.dll", SearchOption.TopDirectoryOnly).
                Select(Assembly.LoadFrom).
                SelectMany(assembly => assembly.GetTypes(), (assembly, type) => new { assembly, type }).
                Where(@t => typeof(T).IsAssignableFrom(@t.type)).
                Select(@t => (T)Activator.CreateInstance(@t.type));
            return a;
            */
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(file);
            }    
            catch(Exception ex)
            {
                Logger.Log.Write(LogLevels.Error,"Невозможно загрузить сборку: "+ex.ToString());
            }
            if (assembly != null)
            {
                foreach (var t in assembly.GetTypes())
                    if (t.IsAssignableFrom(typeof (T)))
                        return (T) Activator.CreateInstance(typeof (T));
            }
            throw new LoadException(String.Format("Тип {0} не найден в файле {1}",typeof(T),file));
        }

        /// <summary>
        /// Инициализация системы
        /// </summary>
        public static void Init()
        {
            /*foreach (SubsystemType type in Enum.GetValues(typeof(SubsystemType)))
            {
                Configure.Cfg.Write<string>(type.ToString(), "Load", "FromFile", "false");
                Configure.Cfg.Write<string>(type.ToString(), "Load", "File", "");
                Configure.Cfg.Write<string>(type.ToString(), "Module", "FromFile", "false");
                Configure.Cfg.Write<string>(type.ToString(), "Module", "File", "");
            }*/
            var start = DateTime.Now;
            LoadSubsystems();
            var end = DateTime.Now;
            Logger.Log.Write(LogLevels.Debug,String.Format("Все подсистемы загружены за {0} мсек", (end - start).TotalMilliseconds));
        }

        /// <summary>
        /// Загрузка конкретной подсистемы
        /// </summary>
        /// <param name="type">Тип загружаемой подсистемы</param>
        public static void LoadSubsystem(SubsystemType type)
        {
            var loadFromFile = Configure.Cfg.Read<string>(type.ToString(), "Load", "FromFile");
            ISubsystem subsystem = null;
            if(loadFromFile == "true")//Если в конфиге указана загрузка из файла
            {
                var file = Configure.Cfg.Read<string>(type.ToString(), "Load", "File");
                try
                {
                    subsystem = LoadType<ISubsystem>(file); //Загрузка подсистемы из сборки
                }
                catch (LoadException)
                {
                    subsystem = null;
                }
                finally
                {//Если подсистема не загрузилась, или загрузилась не та , что нужно 
                    if ((subsystem == null) || (subsystem.Type() != type))
                    {//Пишем сообщение в лог
                        Logger.Log.Write(LogLevels.Error,
                                         String.Format(
                                             "В файле {0} не содержится модуля подсистемы типа {1}, будет загружена подсистема по умлочанию",
                                             file, type));
                        loadFromFile = "false";//И загружаем подсистему по умолчанию
                    }
                }
            }
            if (loadFromFile == "false")//Загрузка подсистемы по умолчанию из ядра
            {
                subsystem = SubsystemFactory.GetSubsystem(type);
            }
            Kernel.GetKernel.RegisterSubsystem(subsystem);
        }

        /// <summary>
        /// Загружает указанный тип модуля для подсистемы
        /// </summary>
        /// <typeparam name="T">Тип загружаемого модуля</typeparam>
        /// <param name="subsystem">Подсистема, для которой загружается модуль</param>
        /// <returns></returns>
        public static T LoadSubsystemModule<T>(ISubsystem subsystem) where T:class
        {
            var loadFromFile = Configure.Cfg.Read<string>(subsystem.Type().ToString(), "Module", "FromFile");
            var module = default(T);
            
            if (loadFromFile == "true")//Если в конфиге указана загрузка из файла
            {
                var file = Configure.Cfg.Read<string>(subsystem.Type().ToString(), "Module", "File");
                try
                {
                    module = LoadType<T>(file); //Загрузка подсистемы из сборки
                }
                catch (LoadException)
                {
                    //Загрузка модуля из сборки не удалась
                }
                finally
                {//Если модуль не загрузился 
                    if (module == default(T))
                    {//Пишем сообщение в лог
                        Logger.Log.Write(LogLevels.Error,
                                         String.Format(
                                             "В файле {0} не содержится модуля подсистемы типа {1}, будет загружен модуль по умлочанию",
                                             file, subsystem.Type()));
                        loadFromFile = "false";//И загружаем подсистему по умолчанию
                    }
                }
            }
            if (loadFromFile == "false")//Загрузка подсистемы по умолчанию из ядра
            {
                module = SubsystemFactory.GetSubsystemModule<T>(subsystem);
            }
            return module;
        }

        public static void Dispose()
        {
            var startTime = DateTime.Now;
            while(Kernel.GetKernel.Subsystems.Count > 0)
                Kernel.GetKernel.Subsystems[0].Dispose();
            var endTime = DateTime.Now;
            Logger.Log.Write(LogLevels.Debug,
                String.Format("Все подсистемы успешно выгружены за {0} мсек",
                (endTime - startTime).TotalMilliseconds));
        }

        /// <summary>
        /// Загрузка всех имеющихся подсистем
        /// </summary>
        private static void LoadSubsystems()
        {
            //Подсистема управления базой данных должна быть 
            //загружена самой первой
            LoadSubsystem(SubsystemType.DataBase);
            LoadSubsystem(SubsystemType.Access);
            LoadSubsystem(SubsystemType.Project);
            LoadSubsystem(SubsystemType.WordProcessor);
            LoadSubsystem(SubsystemType.Network);
            LoadSubsystem(SubsystemType.Chat);
            LoadSubsystem(SubsystemType.Report);
            LoadSubsystem(SubsystemType.Vcs);
        }


    }
}
