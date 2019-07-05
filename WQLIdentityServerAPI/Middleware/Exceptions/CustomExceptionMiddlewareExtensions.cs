using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentityServerAPI.Middleware.Exceptions
{
    public static class CustomExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionMidlleware(this IApplicationBuilder app,ILogger logger)
        {
            app.UseMiddleware<CustomExceptionMidlleware>(logger);
        }
    }
}
