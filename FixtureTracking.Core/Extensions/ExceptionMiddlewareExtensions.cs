using FixtureTracking.Core.Utilities.Middlewares.Exception;
using Microsoft.AspNetCore.Builder;

namespace FixtureTracking.Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
