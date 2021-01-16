using NLog;
using NLog.LayoutRenderers;
using System.Text;

namespace FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Layouts
{
    [LayoutRenderer("claim")]
    public class ClaimLayout : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            LogDetail logDetail = (LogDetail)logEvent.Parameters[0];
            builder.Append(logDetail.ClaimId);
        }
    }
}
