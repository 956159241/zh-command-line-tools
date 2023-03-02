using NLog;
using System.Reflection;

namespace log
{
	public class LogHelper
	{
		public enum LogLevel
		{
			Trace,
			Debug,
			Info,
			Error,
			Fatal
		}

		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static void WriteLog(string message, LogLevel logLevel)
		{
			if (string.IsNullOrEmpty(message))
				return;
			var type = logger.GetType();
			type.InvokeMember(logLevel.ToString(), BindingFlags.Default | BindingFlags.InvokeMethod, null, logger, new object[] { message });
		}

		public static void WriteLog(string message, Exception ex, LogLevel logLevel)
		{
			if (string.IsNullOrEmpty(message))
				return;
			var type = logger.GetType();
			type.InvokeMember(logLevel.ToString(), BindingFlags.Default | BindingFlags.InvokeMethod, null, logger, new object[] { message, ex });
		}
	}
}