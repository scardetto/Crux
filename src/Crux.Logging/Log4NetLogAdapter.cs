
using System;

namespace Crux.Logging 
{
	/// <summary>
	/// Summary description for Log4NetLogAdapter.
	/// </summary>
	public class Log4NetLogAdapter : ILog 
	{
		private readonly log4net.ILog _log;

		public Log4NetLogAdapter(log4net.ILog log) 
		{
			_log = log;
		}
		
		public void Debug(object message) 
		{
			_log.Debug(message.ToString());
		}

		public void Debug(string message, params object[] args) 
		{
			_log.Debug(String.Format(message, args));
		}

		public void Debug(IFormatProvider provider, string message, params object[] args) 
		{
			_log.Debug(String.Format(provider, message, args));
		}

		public void Debug(object message, Exception e) 
		{
			_log.Debug(message, e);
		}

		public void Debug(string message, Exception e, params object[] args) 
		{
			_log.Debug(String.Format(message, args), e);
		}

		public void Debug(IFormatProvider provider, string message, Exception e, params object[] args) 
		{
			_log.Debug(String.Format(provider, message, args), e);
		}

		public bool IsDebugEnabled 
		{
			get { return _log.IsDebugEnabled; }
		}
	
		public void Info(object message) 
		{
			_log.Info(message.ToString());
		}

		public void Info(string message, params object[] args) 
		{
			_log.Info(String.Format(message, args));
		}

		public void Info(IFormatProvider provider, string message, params object[] args) 
		{
			_log.Info(String.Format(provider, message, args));
		}

		public void Info(object message, Exception e) 
		{
			_log.Info(message, e);
		}

		public void Info(string message, Exception e, params object[] args) 
		{
			_log.Info(String.Format(message, args), e);
		}

		public void Info(IFormatProvider provider, string message, Exception e, params object[] args) 
		{
			_log.Info(String.Format(provider, message, args), e);
		}

		public bool IsInfoEnabled 
		{
			get { return _log.IsInfoEnabled; }
		}
	
		public void Warn(object message) 
		{
			_log.Warn(message.ToString());
		}

		public void Warn(string message, params object[] args) 
		{
			_log.Warn(String.Format(message, args));
		}

		public void Warn(IFormatProvider provider, string message, params object[] args) 
		{
			_log.Warn(String.Format(provider, message, args));
		}

		public void Warn(object message, Exception e) 
		{
			_log.Warn(message, e);
		}

		public void Warn(string message, Exception e, params object[] args) 
		{
			_log.Warn(String.Format(message, args), e);
		}

		public void Warn(IFormatProvider provider, string message, Exception e, params object[] args) 
		{
			_log.Warn(String.Format(provider, message, args), e);
		}

		public bool IsWarnEnabled 
		{
			get { return _log.IsWarnEnabled; }
		}
	
		public void Error(object message) 
		{
			_log.Error(message.ToString());
		}

		public void Error(string message, params object[] args) 
		{
			_log.Error(String.Format(message, args));
		}

		public void Error(IFormatProvider provider, string message, params object[] args) 
		{
			_log.Error(String.Format(provider, message, args));
		}

		public void Error(object message, Exception e) 
		{
			_log.Error(message, e);
		}

		public void Error(string message, Exception e, params object[] args) 
		{
			_log.Error(String.Format(message, args), e);
		}

		public void Error(IFormatProvider provider, string message, Exception e, params object[] args) 
		{
			_log.Error(String.Format(provider, message, args), e);
		}

		public bool IsErrorEnabled 
		{
			get { return _log.IsErrorEnabled; }
		}
	
		public void Fatal(object message) 
		{
			_log.Fatal(message.ToString());
		}

		public void Fatal(string message, params object[] args) 
		{
			_log.Fatal(String.Format(message, args));
		}

		public void Fatal(IFormatProvider provider, string message, params object[] args) 
		{
			_log.Fatal(String.Format(provider, message, args));
		}

		public void Fatal(object message, Exception e) 
		{
			_log.Fatal(message, e);
		}

		public void Fatal(string message, Exception e, params object[] args) 
		{
			_log.Fatal(String.Format(message, args), e);
		}

		public void Fatal(IFormatProvider provider, string message, Exception e, params object[] args) 
		{
			_log.Fatal(String.Format(provider, message, args), e);
		}

		public bool IsFatalEnabled 
		{
			get { return _log.IsFatalEnabled; }
		}
		}
}