using System;
using System.Runtime.Serialization;

namespace IDEService.Core
{
    /// <summary>
    /// Сбор сведений о подсистеме
    /// </summary>
    [DataContract]
    public class SubsystemInfo
    {
        /// <summary>
        /// Сосотояние подсистемы
        /// </summary>
        [DataMember]
        public SubsystemState State { get; set; }

        /// <summary>
        /// Тип подсистемы
        /// </summary>
        [DataMember]
        public SubsystemType Type { get; set; }

        /// <summary>
        /// Количество полученных сообщений с момента запуска
        /// </summary>
        [DataMember]
        public int RecievedMessages { get; set; }

        /// <summary>
        /// Среднее время на обработку 1 сообщения
        /// </summary>
        [DataMember]
        public double MeanTimeToMessage { get; set; }

        /// <summary>
        /// Время, затраченное на обработку всех сообщений
        /// </summary>
        [DataMember]
        public double ProcessTime { get; set; }

        /// <summary>
        /// Время запуска подсистемы
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }

        public override string ToString()
        {
            return String.Format("Тип {0}\t Состояние {1}\t Обработано соощений {2}\t Время работы {3} мсек", Type, State,
                                 RecievedMessages, ProcessTime);
        }
    }
}
