using System;
using IDEClient.ServiceReference1;

namespace IDEClient
{
    /// <summary>
    /// Интерфейс подсистемы
    /// </summary>
    public interface ISubsystem : IDisposable
    {
        /// <summary>
        /// Получение сообщения подсистемой
        /// </summary>
        /// <param name="message">Входящее сообщение</param>
        /// <returns>Ответ на принятое сообщение</returns>
        void SendMessage(ServiceMessage message);

        /// <summary>
        /// Возвращает тип подсистемы
        /// </summary>
        /// <returns></returns>
        SubsystemType Type();

    }
}
