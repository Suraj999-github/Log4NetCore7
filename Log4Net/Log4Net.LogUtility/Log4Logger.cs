using log4net;
using System.Reflection;

namespace Log4Net.LogUtility
{
    public interface ILog4Logger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message, Exception? ex = null);
        void Fatal(string message);
        void Warn(string message);
    }
    public class Log4Logger: ILog4Logger
    {
        private readonly ILog _logger;
        public Log4Logger()
        {
            this._logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        }
        public void Debug(string message)
        {
            this._logger?.Debug(message);
        }
        public void Info(string message)
        {
            this._logger?.Info(message);
        }
        public void Error(string message, Exception? ex = null)
        {
            this._logger?.Error(message, ex?.InnerException);
        }
        public void Fatal(string message)
        {
            this._logger?.Fatal(message);
        }
        public void Warn(string message)
        {
            this._logger?.Warn(message);
        }
    }
}