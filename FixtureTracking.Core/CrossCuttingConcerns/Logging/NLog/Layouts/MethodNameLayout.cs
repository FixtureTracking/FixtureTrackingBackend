using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Layouts
{
    [LayoutRenderer("method-name")]
    public class MethodNameLayout : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var logDetail = (LogDetail)logEvent.Parameters[0];
            builder.Append(logDetail.MethodName);
        }
    }
}
