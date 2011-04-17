using System;
using System.IO;

namespace IDEService.Core
{
    public class Logger
    {
        #region Singleton
        private static Logger _logger;

        private static readonly object _logLocker = new object();

        public static Logger Log
        {
            get
            {
                if (_logger == null)
                    lock (_logLocker)
                        if (_logger == null)
                            _logger = new Logger();
                return _logger;
            }
        }

        #endregion

        private static string _file;

        private static readonly object _fileLock = new object();

        private readonly LogLevels _level;

        public delegate void WriteLog(LogLevels level, string msg);
        
        /// <summary>
        /// Событие записи сообщения в лог-файл
        /// </summary>
        public event WriteLog OnLog;

        private Logger()
        {
            _file = Configure.Cfg.Read<string>("Logger", "LogFile", "File");
            _level = (LogLevels)Enum.Parse(typeof(LogLevels), Configure.Cfg.Read<string>("Logger", "LogLevel", "Level")); 
        }

        public void Write(LogLevels level, string msg)
        {
          if(_level <= level)
              lock (_fileLock)
              {
                  System.IO.File.AppendAllText(_file, string.Format("[{0}] [{1}] {2} \n", DateTime.Now, level, msg));
                  if(OnLog != null)
                    OnLog(level, msg);
              }
        }
    }
}
