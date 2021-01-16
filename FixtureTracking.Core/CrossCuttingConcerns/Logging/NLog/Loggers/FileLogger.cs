namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger() : base("LogFile")
        {
        }
    }
}
