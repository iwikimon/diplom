using System;

namespace IDEService.Core
{
    /// <summary>
    /// Интерфейс подсистемы
    /// </summary>
    public interface ISubsystem : IDisposable
    {
        /// <summary>
        /// Запуск подсистемы
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// Останов подсистемы
        /// </summary>
        /// <returns></returns>
        bool Stop();

        /// <summary>
        /// Перезапуск подсистемы
        /// </summary>
        /// <returns></returns>
        bool Reload();

        /// <summary>
        /// Получение сообщения подсистемой
        /// </summary>
        /// <param name="message">Входящее сообщение</param>
        /// <returns>Ответ на принятое сообщение</returns>
        ServiceMessage SendMessage(ServiceMessage message);

        /// <summary>
        /// Возвращает тип подсистемы
        /// </summary>
        /// <returns></returns>
        SubsystemType Type();

        /// <summary>
        /// Получение информации о состоянии подсистемы
        /// </summary>
        /// <returns></returns>
        SubsystemState GetState();

        /// <summary>
        /// Получение информации о подсистеме
        /// </summary>
        /// <returns>Информация о подсистеме</returns>
        SubsystemInfo GetInfo();
    }
}
