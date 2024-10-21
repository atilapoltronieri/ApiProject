using ApiProject.Api.Middlewares;
using System.Diagnostics.CodeAnalysis;

namespace ApiProject.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
