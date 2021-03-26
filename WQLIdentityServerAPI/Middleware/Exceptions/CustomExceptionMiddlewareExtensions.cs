﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace WQLIdentityServerAPI.Middleware.Exceptions
{
    public static class CustomExceptionMiddlewareExtensions
    {
        public static void UseCustomException(this IApplicationBuilder app, ILogger logger)
        {
            app.UseMiddleware<CustomExceptionMidlleware>(logger);
        }
    }
}
