using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ApiProject.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            // Log the incoming request
            LogRequest(context.Request);


            // Call the next middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            LogResponse(context.Response);
        }

        private void LogRequest(HttpRequest request)
        {
            _logger.LogInformation($"Request received: {request.Method} {request.Path}");
            _logger.LogInformation($"Request headers: {GetHeadersAsString(request.Headers)}");
        }

        private void LogResponse(HttpResponse response)
        {
            _logger.LogInformation($"Response sent: {response.StatusCode}");
            _logger.LogInformation($"Response headers: {GetHeadersAsString(response.Headers)}");
        }

        private static string GetHeadersAsString(IHeaderDictionary headers)
        {
            var stringBuilder = new StringBuilder();
            foreach (var (key, value) in headers)
            {
                stringBuilder.AppendLine($"{key}: {value}");
            }
            return stringBuilder.ToString();
        }
    }
}
