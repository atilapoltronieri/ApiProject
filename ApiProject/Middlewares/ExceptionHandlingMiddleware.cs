﻿using ApiProject.Core.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ApiProject.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

    [ExcludeFromCodeCoverage]
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            ExceptionResponse response = exception switch
            {
                NotFoundException ex => new ExceptionResponse(HttpStatusCode.NotFound, ex.Message),
                BusinessException ex => new ExceptionResponse(HttpStatusCode.InternalServerError, ex.Message),
                ApplicationException _ => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
                UnauthorizedAccessException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}