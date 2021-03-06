using System;
using System.Net;
using System.Threading.Tasks;
using CheckoutPaymentGatewayApi.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CheckoutPaymentGatewayApi.Middleware
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomErrorHandlerMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ExceptionInfo
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}
