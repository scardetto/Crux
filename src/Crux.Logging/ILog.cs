
    
using System;

namespace Crux.Logging 
{
    public interface ILog 
    {
    
    
        void Debug(object message);
        void Debug(string message, params object[] args);
        void Debug(object message, Exception e);
        void Debug(string message, Exception e, params object[] args);
        bool IsDebugEnabled { get; }

    
        void Info(object message);
        void Info(string message, params object[] args);
        void Info(object message, Exception e);
        void Info(string message, Exception e, params object[] args);
        bool IsInfoEnabled { get; }

    
        void Warn(object message);
        void Warn(string message, params object[] args);
        void Warn(object message, Exception e);
        void Warn(string message, Exception e, params object[] args);
        bool IsWarnEnabled { get; }

    
        void Error(object message);
        void Error(string message, params object[] args);
        void Error(object message, Exception e);
        void Error(string message, Exception e, params object[] args);
        bool IsErrorEnabled { get; }

    
        void Fatal(object message);
        void Fatal(string message, params object[] args);
        void Fatal(object message, Exception e);
        void Fatal(string message, Exception e, params object[] args);
        bool IsFatalEnabled { get; }

    
    }
}