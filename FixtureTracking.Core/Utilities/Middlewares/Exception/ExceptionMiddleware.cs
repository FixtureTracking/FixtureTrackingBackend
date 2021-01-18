using FixtureTracking.Core.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FixtureTracking.Core.Utilities.Middlewares.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (System.Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, System.Exception e)
        {
            ErrorDetail errorDetail = new ErrorDetail();

            if (e is HttpStatusException httpStatusException)
            {
                errorDetail.StatusCode = httpStatusException.HttpStatusCode;
                errorDetail.Message = httpStatusException.Message;
            }
            else
            {
                errorDetail.StatusCode = 500;
                errorDetail.Message = "Internal server error";
            }

            httpContext.Response.ContentType = errorDetail.ContentType;
            httpContext.Response.StatusCode = errorDetail.StatusCode;
            return httpContext.Response.WriteAsync(errorDetail.ToString());
        }
    }
}
