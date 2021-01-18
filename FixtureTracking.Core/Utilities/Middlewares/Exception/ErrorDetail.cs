using Newtonsoft.Json;

namespace FixtureTracking.Core.Utilities.Middlewares.Exception
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ContentType { get; private set; }

        public ErrorDetail()
        {
            ContentType = "application/json";
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { statusCode = StatusCode, message = Message });
        }
    }
}
