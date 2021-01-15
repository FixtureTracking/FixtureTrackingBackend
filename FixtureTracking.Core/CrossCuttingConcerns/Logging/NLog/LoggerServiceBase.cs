using NLog;
using NLog.Web;

namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog
{
    public class LoggerServiceBase
    {
        private readonly Logger logger;

        public LoggerServiceBase(string name)
        {
            logger = NLogBuilder.ConfigureNLog("nlog.config").GetLogger(name);
        }

        public void Debug(string message)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.Debug(message);
        }

        public void Error(string message)
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.Error(message);
        }

        public void Fatal(string message)
        {
            if (logger.IsEnabled(LogLevel.Fatal))
                logger.Fatal(message);
        }

        public void Info(string message)
        {
            if (logger.IsEnabled(LogLevel.Info))
                logger.Info(message);
        }

        public void Trace(string message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                logger.Trace(message);
        }

        public void Warn(string message)
        {
            if (logger.IsEnabled(LogLevel.Warn))
                logger.Warn(message);
        }
    }
}
