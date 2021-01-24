using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Layouts
{
    [LayoutRenderer("nlog-connection-string")]
    public class ConnectionStringLayout : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(_connectionString);
        }


        private static string _connectionString;
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
