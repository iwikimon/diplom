using System;

namespace IDEService.Core
{
    /// <summary>
    /// Обобщенный клас сообщения об ошибке ядра
    /// </summary>
    public class KernelExceptions :Exception
    {
        public KernelExceptions(string errorMsg):base(errorMsg){ }
    }

    /// <summary>
    /// Класс сообщения об ошибке регистрации подсистемы в ядре
    /// </summary>
    public class SubsystemRegistredException :KernelExceptions
    {
        public SubsystemRegistredException(string errorMsg):base(errorMsg) { }
    }

    /// <summary>
    /// Ошибка отключенияя подсистемы от ядра
    /// </summary>
    public class SubsystemUnRegistredException :KernelExceptions
    {
        public SubsystemUnRegistredException(string errorMsg):base(errorMsg) { }
    }
    
    /// <summary>
    /// Ошибка загрузки подсистемы
    /// </summary>
    public class SubsystemLoadException :KernelExceptions
    {
        public SubsystemLoadException(string errorMsg):base(errorMsg) { }
    }

    /// <summary>
    /// Ошибка запуска подсистемы
    /// </summary>
    public class SubsystemStartException : KernelExceptions
    {
        public SubsystemStartException(string errorMsg) : base(errorMsg) { }
    }

    /// <summary>
    /// Ошибка перезагрузки подсистемы
    /// </summary>
    public class SubsystemReloadException : KernelExceptions
    {
        public SubsystemReloadException(string errorMsg) : base(errorMsg) { }
    }

    /// <summary>
    /// Подсистема не найдена
    /// </summary>
    public class SubsystemNotFoundException : KernelExceptions
    {
        public SubsystemNotFoundException(string errorMsg) : base(errorMsg) { }
    }

    /// <summary>
    /// Ошибка работы подсистемы
    /// </summary>
    public class SubsystemWorkingException : KernelExceptions
    {
        public SubsystemWorkingException(string errorMsg) : base(errorMsg) { }
    }

}
