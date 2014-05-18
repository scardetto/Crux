using System;

namespace Crux.Logging
{
    public class LogManager
    {
        private LogManager() { }

        public static ILog GetLogger(string name)
        {
            return new Log4NetLogAdapter(log4net.LogManager.GetLogger(name));
        }

        public static ILog GetLogger(Type type)
        {
            return new Log4NetLogAdapter(log4net.LogManager.GetLogger(type));
        }
    }
}
