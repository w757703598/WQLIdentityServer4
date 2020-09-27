using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Middleware.Exceptions
{
    public class CustomExceptionMidlleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;
        public CustomExceptionMidlleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            string result = null;

            context.Response.ContentType = "application/json;charset=UTF-8";
            result = new ErrorContent() { Message = exception.Message, StatusCode = (int)HttpStatusCode.BadRequest }.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
