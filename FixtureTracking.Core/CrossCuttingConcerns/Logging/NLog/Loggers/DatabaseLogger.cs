namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers
{
    public class DatabaseLogger : LoggerServiceBase
    {
        public DatabaseLogger() : base("LogDatabase")
        {
        }
    }
}
