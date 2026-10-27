using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cooker.Api.Middleware
{
    public class LogTraceMiddleware : IBaseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogTraceMiddleware> _logger;
        public LogTraceMiddleware(RequestDelegate next, ILogger<LogTraceMiddleware> logger)
        {
            this._logger = logger;
            this._next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.BeginScope(new Dictionary<string, object>
            {
                {"TraceId",context.TraceIdentifier},
            });
            await _next(context);
        }
    }
}