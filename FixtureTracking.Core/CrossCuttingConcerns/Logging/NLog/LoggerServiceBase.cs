using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Layouts;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web;

namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog
{
    public class LoggerServiceBase
    {
        private readonly Logger logger;

        public LoggerServiceBase(string name)
        {
            LayoutRenderer.Register<MethodNameLayout>("method-name");

            logger = NLogBuilder.ConfigureNLog("nlog.config").GetLogger(name);
        }

        public void Debug(object message)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.Debug("{@message}", message);
        }

        public void Error(object message)
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.Error("{@message}", message);
        }

        public void Fatal(object message)
        {
            if (logger.IsEnabled(LogLevel.Fatal))
                logger.Fatal("{@message}", message);
        }

        public void Info(object message)
        {
            if (logger.IsEnabled(LogLevel.Info))
                logger.Info("{@message}", message);
        }

        public void Trace(object message)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                logger.Trace("{@message}", message);
        }

        public void Warn(object message)
        {
            if (logger.IsEnabled(LogLevel.Warn))
                logger.Warn("{@message}", message);
        }
    }
}
